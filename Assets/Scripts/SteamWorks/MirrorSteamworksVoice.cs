using System;
using System.Collections.Generic;
using System.IO;
using Mirror;
using Steamworks;
using UnityEngine;

// Код не наш, спасибо GitHub GIST
public class MirrorSteamworksVoice : NetworkBehaviour
{
    [SerializeField] 
    private AudioSource _audioSource;

    private readonly MemoryStream _compressedVoiceStream = new();
    private readonly MemoryStream _decompressedVoiceStream = new();
    private readonly Queue<float> _streamingReadQueue = new();

    private void Start()
    {
        _audioSource.clip = AudioClip.Create("SteamVoice", Convert.ToInt32(SteamUser.SampleRate),
            1, Convert.ToInt32(SteamUser.SampleRate), true, PcmReaderCallback);

        _audioSource.Play();
    }

    private void Update()
    {
        if (!isLocalPlayer) return;
        SteamUser.VoiceRecord = Input.GetKey(KeyCode.V);

        if (SteamUser.HasVoiceData)
        {
            _compressedVoiceStream.Position = 0;

            int numBytesWritten = SteamUser.ReadVoiceData(_compressedVoiceStream);

            CmdSubmitVoice(new ArraySegment<byte>(_compressedVoiceStream.GetBuffer(), 0, numBytesWritten));
        }
    }

    [Command(channel = Channels.Unreliable, requiresAuthority = true)]
    private void CmdSubmitVoice(ArraySegment<byte> voiceData)
    {
        RpcBroadcastVoice(voiceData);
    }

    [ClientRpc(channel = Channels.Unreliable, includeOwner = false)]
    private void RpcBroadcastVoice(ArraySegment<byte> voiceData)
    {
        _compressedVoiceStream.Position = 0;
        _compressedVoiceStream.Write(voiceData);

        _compressedVoiceStream.Position = 0;
        _decompressedVoiceStream.Position = 0;

        int numBytesWritten = SteamUser.DecompressVoice(_compressedVoiceStream, voiceData.Count, _decompressedVoiceStream);

        _decompressedVoiceStream.Position = 0;

        while (_decompressedVoiceStream.Position < numBytesWritten)
        {
            byte byte1 = (byte)_decompressedVoiceStream.ReadByte();
            byte byte2 = (byte)_decompressedVoiceStream.ReadByte();

            short pcmShort = (short)((byte2 << 8) | (byte1 << 0));
            float pcmFloat = Convert.ToSingle(pcmShort) / short.MaxValue;

            _streamingReadQueue.Enqueue(pcmFloat);
        }
    }

    private void PcmReaderCallback(float[] data)
    {
        for (int i = 0; i < data.Length; i++)
        {
            if (_streamingReadQueue.TryDequeue(out float sample))
            {
                data[i] = sample;
            }
            else
            {
                data[i] = 0.0f;  // Nothing in the queue means we should just play silence
            }
        }
    }
}