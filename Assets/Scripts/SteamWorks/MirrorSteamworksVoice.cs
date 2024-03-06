using System;
using Mirror;
using Steamworks;
using System.IO;
using UnityEngine;

public class MirrorSteamworksVoice : NetworkBehaviour
{
    [SerializeField]
    private AudioSource source;
    private MemoryStream output;
    private MemoryStream stream;
    private MemoryStream input;

    private int optimalRate;
    private int clipBufferSize;
    private float[] clipBuffer;

    private int playbackBuffer;
    private int dataPosition;
    private int dataReceived;

    private void Start()
    {
        optimalRate = (int)SteamUser.OptimalSampleRate;

        clipBufferSize = optimalRate * 5;
        clipBuffer = new float[clipBufferSize];

        stream = new MemoryStream();
        output = new MemoryStream();
        input = new MemoryStream();

        // Assign OnAudioread as pcmreader callback
        source.clip = AudioClip.Create("VoiceData", (int)256, 1, (int)optimalRate, true, OnAudioRead, null);
        source.loop = true;
        source.Play();
    }

    // User Input: Automatically send data to server in CmdVoice()
    private void Update()
    {
        if (!isLocalPlayer) return;

        SteamUser.VoiceRecord = Input.GetKey(KeyCode.V);

        if (SteamUser.HasVoiceData)
        {
            int compressedWritten = SteamUser.ReadVoiceData(stream);
            stream.Position = 0;

            CmdVoice(new ArraySegment<byte>(stream.GetBuffer(), 0, compressedWritten));
        }
    }

    // Executed on Server: Sends voice data to all players
    [Command(channel = 1)]
    public void CmdVoice(ArraySegment<byte> compressed)
    {
        RpcVoiceData(compressed);
    }

    /* ### CALLED ON CLIENT WHEN DATA RECIEVED ### */
    //[ClientRpc(channel = 1, includeOwner = false)]
    [ClientRpc(channel = 1)]
    public void RpcVoiceData(ArraySegment<byte> compressed)
    {
        input.Write(compressed.ToArray(), 0, compressed.Count);
        input.Position = 0;

        int uncompressedWritten = SteamUser.DecompressVoice(input, compressed.Count, output);
        input.Position = 0;

        byte[] outputBuffer = output.GetBuffer();
        WriteToClip(outputBuffer, uncompressedWritten);
        output.Position = 0;
    }

    /* ### PCMREADER CALLBACK ### */
    [Client]
    private void OnAudioRead(float[] data)
    {
        for (int i = 0; i < data.Length; ++i)
        {
            // start with silence
            data[i] = 0;

            // do I  have anything to play?
            if (playbackBuffer > 0)
            {
                // current data position playing
                dataPosition = (dataPosition + 1) % clipBufferSize;

                data[i] = clipBuffer[dataPosition];

                playbackBuffer--;
            }
        }

    }

    /* ### WRITES NEW AUDIO DATA INTO CLIP BUFFER ### */
    [Client]
    private void WriteToClip(byte[] uncompressed, int iSize)
    {
        for (int i = 0; i < iSize; i += 2)
        {
            // insert converted float to buffer
            float converted = (short)(uncompressed[i] | uncompressed[i + 1] << 8) / 32767.0f;
            clipBuffer[dataReceived] = converted;

            // buffer loop
            dataReceived = (dataReceived + 1) % clipBufferSize;

            playbackBuffer++;
        }
    }

}