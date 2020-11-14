using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace IniUtils
{
    /// <summary>
    /// iniファイルのキー、値、コメントを保持するクラス
    /// </summary>
    public class IniData : ICloneable
    {
        private string _FileName = "";
        private string _SectionName = "";
        private string _KeyName = "";
        private string _Value = "";
        private string _Comment = "";
        private string _RawString = "";

        /// <summary>
        /// ファイル名
        /// </summary>
        public string FileName { get => _FileName; }
        /// <summary>
        /// セクション名
        /// </summary>
        public string SectionName { get => _SectionName; }
        /// <summary>
        /// キー名
        /// </summary>
        public string KeyName { get => _KeyName; }
        /// <summary>
        /// キーの値
        /// </summary>
        public string Value  { get => _Value; }
        /// <summary>
        /// キーのコメント
        /// </summary>
        public string Comment { get => _Comment; }
        /// <summary>
        /// キー名 = 値 の文字列
        /// </summary>
        public string KeyValue
        {
            get
            {
                if (_RawString != "") { return _RawString; }
                return _KeyName + "=" + _Value;
            }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="file">ファイル名</param>
        /// <param name="section">セクション名</param>
        /// <param name="key">キー名</param>
        /// <param name="value">キーの値</param>
        /// <param name="comment">キーのコメント</param>
        /// <param name="rawString">キー = 値 そのままの文字列</param>
        public IniData(string file, string section , string key , string value, string comment = "", string rawString = "")
        {
            _FileName = file;
            _SectionName = section;
            _KeyName = key;
            _Value = value;
            _Comment = comment;
            _RawString = rawString;
        }

        /// <summary>
        /// iniファイルに書き込む
        /// </summary>
        /// <param name="directory">フォルダパス</param>
        /// <remarks>WritePrivateProfileStringを使用</remarks>
        public void Export(string directory)
        {
            string path = Path.Combine(directory, FileName);
            IniFileUtility.SetIniValue(path, SectionName, KeyName, Value);
        }

        /// <summary>
        /// 指定したファイルに書き込む
        /// </summary>
        /// <param name="writer">書き込みストリーム</param>
        /// <param name="outputComment">コメントも書き込むか</param>
        public void Write(StreamWriter writer, bool outputComment)
        {
            // コメント書き出し
            if (outputComment && _Comment != "")
            {
                string[] del = { "\r\n" };
                foreach (string line in _Comment.Split(del, StringSplitOptions.None))
                {
                    writer.WriteLine(";" + line);
                }
            }
            // キー＆値書き出し
            writer.WriteLine(KeyValue);
        }

        /// <summary>
        /// 比較相手とキー＆値が等しいかを判定する
        /// </summary>
        /// <param name="data">比較相手</param>
        /// <returns>等しければtrue</returns>
        /// <remarks>互いのKeyNameとValueをCaseIgnoreで比較する</remarks>
        public bool IsSameKeyValue(IniData data)
        {
            if (data == null) { return false; }
            return (data.KeyName.ToUpper() == this.KeyName.ToUpper() 
                && data.Value.ToUpper() == this.Value?.ToUpper());
        }

        /// <summary>
        /// ディープコピー
        /// </summary>
        /// <returns>コピーされたオブジェクト</returns>
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
