using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cryptographer
{
    public static string encode(string data)
    {
        string str = "";
        for (int i = 0; i < data.Length; i++){
			str += Random.Range(0, 9).ToString()
                + Random.Range(0, 9).ToString()
                + (char)((int)data[i] + 10)
                + Random.Range(0, 9);
        }

        return System.Convert.ToString(data.Length, 16).PadLeft(4, '0') + str
            + Random.Range(10000000, 99999999) + Random.Range(10000000, 99999999);
    }

    public static string decode(string data)
    {
        int num = System.Convert.ToInt32(data.Substring(0, 4), 16);
		string result = "";
        for (int i = 6; i < data.Length && result.Length < num; i += 4){
			result += (char)((int)(data[i]) - 10);
        }
        return result;
    }
}
