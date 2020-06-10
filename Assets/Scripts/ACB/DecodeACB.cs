using System;
using System.IO;
using System.Threading;
using DereTore.Exchange.Audio.HCA;
using DereTore.Exchange.Archive.ACB;
using UnityEngine;

public class DecodeACB
{
    public static class CgssCipher
    {
        // ミリシタ
        public static readonly uint MilliKey1 = 0xBC731A85;
        public static readonly uint MilliKey2 = 0x0002B875;

        //デレステ
        public static readonly uint Key1 = 0xf27e3b22;
        public static readonly uint Key2 = 0x00003657;
    }

    public delegate void AsyncDecode(string instr, string outstr);

    public static void Decode(string input, string output)
    {
        AsyncDecode asyncDecode = DecodeProc;
        asyncDecode.BeginInvoke(input,output, null, null);
    }

    public static void DecodeMillionProc(byte[] AcbBytes, string fileName, string outputfilename) {
        if (File.Exists(outputfilename)) { return; }

        //復号Keyをセット
        var decodeParams = DecodeParams.CreateDefault();
        decodeParams.Key1 = CgssCipher.MilliKey1;
        decodeParams.Key2 = CgssCipher.MilliKey2;

        //HCAの出力設定
        var audioParams = AudioParams.CreateDefault();
        audioParams.InfiniteLoop = false;
        audioParams.SimulatedLoopCount = 0; //ループ回数
        audioParams.OutputWaveHeader = true;

        //acbファイルを展開
        using (var acb = AcbFile.FromStream(new MemoryStream(AcbBytes), fileName, false)) {
            var fileNames = acb.GetFileNames();
            var s = fileNames[0];
            MemoryStream ms = new MemoryStream();
            byte[] outdata = null;

            //hcaファイルを展開
            using (var source = acb.OpenDataStream(s)) {
                try {
                    using (var hcaStream = new HcaAudioStream(source, decodeParams, audioParams)) {
                        //出力
                        outdata = new byte[hcaStream.Length];
                        hcaStream.Read(outdata, 0, (int)hcaStream.Length);
                    }
                } catch (Exception e) {
                    Debug.Log("HCA Decodeでエラーが発生しました。");
                    Debug.Log("File : " + s);
                    Debug.Log(e);
                    ms.Close();
                    return;
                }
            }

            if (outdata != null)
                File.WriteAllBytes(outputfilename, outdata);

        }
    }


    public static void DecodeProc(string inputfilename, string outputfilename)
    {
        if (!File.Exists(inputfilename)) { return; }
        if (File.Exists(outputfilename)) { return; }

        //復号Keyをセット
        var decodeParams = DecodeParams.CreateDefault();
            decodeParams.Key1 = CgssCipher.Key1;
            decodeParams.Key2 = CgssCipher.Key2;

        //HCAの出力設定
        var audioParams = AudioParams.CreateDefault();
        audioParams.InfiniteLoop = false;
        audioParams.SimulatedLoopCount = 0; //ループ回数
        audioParams.OutputWaveHeader = true;

        //acbファイルを展開
        using (var acb = AcbFile.FromFile(inputfilename))
        {
            var fileNames = acb.GetFileNames();
            var s = fileNames[0];
            MemoryStream ms = new MemoryStream();
            byte[] outdata = null;

            //hcaファイルを展開
            using (var source = acb.OpenDataStream(s))
            {
                try
                {
                    using (var hcaStream = new HcaAudioStream(source, decodeParams, audioParams))
                    {
                        //出力
                        outdata = new byte[hcaStream.Length];
                        hcaStream.Read(outdata, 0, (int)hcaStream.Length);
                    }
                }
                catch (Exception e)
                {
                    Debug.Log("HCA Decodeでエラーが発生しました。");
                    Debug.Log("File : " + s);
                    Debug.Log(e);
                    ms.Close();
                    return;
                }
            }

            if (outdata != null)
                File.WriteAllBytes(outputfilename, outdata);

        }
    }

}