namespace RelertSharp.Encoding
{
    public static class CsfEncoding
    {
        public static string Decode(byte[] _data)
        {
            for (int i = 0; i < _data.Length; i++) _data[i] = (byte)~_data[i];
            return System.Text.Encoding.Unicode.GetString(_data);
        }
    }
}
