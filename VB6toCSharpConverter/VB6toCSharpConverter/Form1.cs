using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace VB6toCSharpConverter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private const string WRITE_FOLDER = "Ｃ＃";
        private Encoding ENC = Encoding.GetEncoding("Shift_JIS");
        private string DESKTOP_PATH = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

        /// <summary>
        /// ListBox1ドラッグエンター時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBox1_DragEnter(object sender, DragEventArgs e)
        {
            //ファイルのときはコピー。その他は無視。
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        /// <summary>
        /// ListBox1ドラッグドロップ時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBox1_DragDrop(object sender, DragEventArgs e)
        {
            //すべてのファイル名を取得
            string[] fileName = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            ListBox1.Items.AddRange(fileName);

            //ファイルコンバートメイン実行
            FileConvertMain();
        }

        /// <summary>
        /// ファイルコンバートメイン
        /// </summary>
        private void FileConvertMain()
        {
            //0件なら終了
            if (ListBox1.Items.Count == 0)
            {
                MessageBox.Show("ファイルがありません。");
                return;
            }

            //確認して出力フォルダ作成
            if (!MakeOutFolder(DESKTOP_PATH + "\\" + WRITE_FOLDER))
            {
                return;
            }

            //ファイル数だけ繰り返す
            for (int i = 0; i < ListBox1.Items.Count; i++)
            {
                //ファイル読み込み
                string readPath = ListBox1.Items[i].ToString();
                string str = ReadFile(readPath);

                //ＶＢをＣ＃へ変換
                str = TextReplace(str);
                str = AiConvert(str);

                //ファイル書き込み
                string writePath = GetWritePath(readPath);
                WriteFile(writePath, str);
            }
            Close();
        }

        /// <summary>
        /// ファイル書き込み
        /// </summary>
        /// <param name="path">書き込みファイルパス</param>
        /// <param name="str">書き込みテキスト</param>
        private void WriteFile(string path, string str)
        {
            StreamWriter sw = new StreamWriter(path, false, ENC);
            sw.WriteLine(str);
            sw.Close();
        }

        /// <summary>
        /// 書き込みファイルパスを取得する
        /// </summary>
        /// <param name="readPath">読み込みファイルパス</param>
        /// <returns>書き込みファイルパス</returns>
        private string GetWritePath(string readPath)
        {
            string readFileName = Path.GetFileName(readPath);
            string readFileNameNoExt = Path.GetFileNameWithoutExtension(readPath);
            string readFileExt = Path.GetExtension(readPath);
            string writeFileExt = "";
            switch (readFileExt)
            {
                case ".frm":
                    writeFileExt = ".cfrm";
                    break;
                case ".bas":
                    writeFileExt = ".cs";
                    break;
            }
            return DESKTOP_PATH + "\\" + WRITE_FOLDER + "\\" + readFileNameNoExt + writeFileExt;
        }

        /// <summary>
        /// 文字列置換
        /// </summary>
        /// <param name="read">置換前文字列</param>
        /// <returns>置換後文字列</returns>
        private string TextReplace(string str)
        {
            //置換文字列定義（"VBの文字列", "C#の文字列"）
            string[] data = new string[]
            {
                "'",                "//",
                "Private Sub ",     "private void ",
                "Public Sub ",      "public void ",
                "If ",              "if (",
                " <> ",             " != ",
                " Then",            ")\n{",
                "End If",           "}",
                " Or ",             " || ",
                " And ",            " && ",
                "Else",             "}\nelse\n{",
                "End Sub",          "}",
                "Select Case ",     "switch (",
                "Case ",            "case ",
                "End Select",       "}",
                "End Select",       "}",
                "Call ",            "",
            };

            //文字列置換
            for (int i = 0; i < data.Length; i += 2)
            {
                str = str.Replace(data[i], data[i + 1]);
            }
            return str;
        }

        /// <summary>
        /// ＡＩ変換
        /// </summary>
        /// <param name="str">変換前文字列</param>
        /// <returns>変換後文字列</returns>
        private string AiConvert(string str)
        {
            //関数定義内の型を修正する
            CorrectFunctionType(str);




            return str;
        }

        /// <summary>
        /// //関数定義内の型を修正する
        /// </summary>
        /// <param name="str">修正前</param>
        /// <returns>修正後</returns>
        private string CorrectFunctionType(string str)
        {
            int idx = str.IndexOf("private void ");
            if (idx >= 0)
            {
                string line = GetLine(str, idx);






            }



            return str;
        }


        /// <summary>
        /// 文字列から指定位置の単語を取得
        /// </summary>
        /// <param name="str">文字列</param>
        /// <param name="idx">指定位置</param>
        /// <returns>取得単語</returns>
        private string GetWord(string str, int idx)
        {




            return str;
        }

        /// <summary>
        /// 文字列から指定位置の行を取得
        /// </summary>
        /// <param name="str">文字列</param>
        /// <param name="idx">指定位置</param>
        /// <returns>取得行</returns>
        private string GetLine(string str, int idx)
        {
            string line = "";
            string character = str.Substring(idx, 1);
            while (character != "\r" && character != "\n")
            {
                line += character;
                idx++;
                if (idx >= str.Length)
                {
                    break;
                }
                character = str.Substring(idx, 1);
            }
            return line;
        }

        /// <summary>
        /// 出力フォルダ作成
        /// </summary>
        /// <param name="path">出力先フォルダパス</param>
        private bool MakeOutFolder(string path)
        {
            if (Directory.Exists(path))
            {
                string message = "デスクトップにすでに出力用フォルダがあります。削除してよろしいですか？";
                string title = "確認";
                MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
                MessageBoxIcon icon = MessageBoxIcon.Question;
                DialogResult result = MessageBox.Show(message, title, buttons, icon);
                if (result != DialogResult.OK)
                {
                    MessageBox.Show("処理を中止します");
                    return false;
                }
                Directory.Delete(path, true);
            }
            Directory.CreateDirectory(path);
            return true;
        }

        /// <summary>
        /// ファイル読み込み
        /// </summary>
        /// <param name="path">ファイルパス</param>
        /// <returns>読み込んだ文字列</returns>
        private string ReadFile(string path)
        {
            StreamReader sr = new StreamReader(path, ENC);
            string str = sr.ReadToEnd();
            sr.Close();
            return str;
        }
    }
}

