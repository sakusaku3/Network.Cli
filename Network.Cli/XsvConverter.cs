using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic.FileIO;

namespace Network.Cli
{
    /// <summary>
    /// xxx Separeted Valueタイプのファイル変換クラス
    /// </summary>
    public static class XsvConverter
	{
		/// <summary>
		/// 指定した形式のテキストファイルを読み込む
		/// </summary>
		/// <param name="filepath">ファイルパス</param>
		/// <param name="enc">エンコーディング</param>
		/// <param name="delimiters">区切り文字</param>
		/// <returns>テキストファイルを一行ずつに分け、リストで返す</returns>
		public static IReadOnlyList<string[]> Read(
			string filepath,
			Encoding enc,
			string delimiters)
		{
            return EnumerateLines(filepath, enc, delimiters).ToArray();
        }

		/// <summary>
		/// 指定した形式のテキストファイルを読み込む
		/// </summary>
		/// <param name="filepath">ファイルパス</param>
		/// <param name="enc">エンコーディング</param>
		/// <param name="delimiters">区切り文字</param>
		/// <returns>テキストファイルを一行ずつに分け、リストで返す</returns>
		public static IEnumerable<string[]> EnumerateLines(
            string filepath,
            Encoding enc,
            string delimiters)
        {
            using var parser = new TextFieldParser(filepath, enc)
            {
                TextFieldType = FieldType.Delimited
            };

            parser.SetDelimiters(delimiters);
            while (!parser.EndOfData)
                yield return parser.ReadFields();
        }

        /// <summary>
        /// 指定した形式でテキストファイルを出力する
        /// </summary>
        /// <param name="lines">出力する文字列リスト</param>
        /// <param name="filepath">ファイルパス</param>
        /// <param name="enc">エンコーディング</param>
        /// <param name="delimiters">区切り文字</param>
        /// <param name="enclosure">囲み文字</param>
        public static void Write(
			IEnumerable<string[]> lines,
			string filepath,
			Encoding enc,
			string delimiters,
			string enclosure = "")
		{
            var strings = lines.Select(x => GetLine(x, delimiters, enclosure));
            using var writer = new StreamWriter(filepath, false, enc);
            foreach (string st in strings)
                writer.WriteLine(st);
        }

        /// <summary>
        /// 一行取得
        /// </summary>
        /// <param name="strings">文字列配列</param>
        /// <param name="delimiters">区切り文字</param>
        /// <returns></returns>
        private static string GetLine(string[] strings, string delimiters, string enclosure = "")
		{
            return string.Join(
                delimiters, 
                strings
                .Select(x => x ?? string.Empty)
                .Select(x => x.Replace("\"", "\"\""))
                .Select(x => $"{enclosure}{x}{enclosure}"));
		}
	}
}
