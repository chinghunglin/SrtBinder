using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms.VisualStyles;

namespace SrtBinder
{
    public partial class MainFrm : Form
    {
        public MainFrm()
        {
            InitializeComponent();

            this.Text = $"SrtBinder v{SrtBinderVersion()} by CHL";


            this.Lbx_SRT1.DragDrop += new
                 System.Windows.Forms.DragEventHandler(this.listBox1_DragDrop);
            this.Lbx_SRT1.DragEnter += new
                 System.Windows.Forms.DragEventHandler(this.HandleDragEnter);
            this.Lbx_SRT2.DragDrop += new
                 System.Windows.Forms.DragEventHandler(this.listBox2_DragDrop);
            this.Lbx_SRT2.DragEnter += new
                 System.Windows.Forms.DragEventHandler(this.HandleDragEnter);
        }

        public static string SrtBinderVersion()
        {
            // 取得目前執行中的組件
            var assembly = Assembly.GetExecutingAssembly();

            // 檔案版本 (File Version)
            var fileVersionInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
            return fileVersionInfo.FileVersion;
        }


        private void HandleDragDrop(DragEventArgs e, ListBox targetListBox, ListBox otherListBox)
        {
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            targetListBox.Items.Clear();

            // 將所有檔案新增到目標 ListBox
            foreach (string file in s)
            {
                targetListBox.Items.Add(file);
            }

            // 如果新增多於一個檔案，只保留第一個
            if (targetListBox.Items.Count > 1)
            {
                object tmp = targetListBox.Items[0];
                targetListBox.Items.Clear();
                targetListBox.Items.Add(tmp);
            }

            // 驗證檔案是否合法
            if (!IsValidSrtFile(s[0]))
            {
                MessageBox.Show(s[0] + " is not a good SRT file!");
                targetListBox.Items.Clear();
                return;
            }

            // 驗證兩個 ListBox 的第一個項目是否相等以設定按鈕狀態
            if ((otherListBox.Items.Count > 0) &&
                SrtLinesEqual(targetListBox.Items[0].ToString(), otherListBox.Items[0].ToString()))
            {
                Btn_Combine.Enabled = true;
            }
            else
            {
                Btn_Combine.Enabled = false;
            }
        }

        private void listBox1_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            HandleDragDrop(e, Lbx_SRT1, Lbx_SRT2);
        }


        private void listBox2_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            HandleDragDrop(e, Lbx_SRT2, Lbx_SRT1);
        }

        public static bool IsDivisibleBy4(int number)
        {
            return number % 4 == 0;
        }

        public static bool OnlyChineseNumber(string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;

            // 定義僅包含中文數字字元的正規表示式
            var pattern = @"^[零一二三四五六七八九十百千]+$";
            var regex = new Regex(pattern);

            // 檢查輸入字串是否符合
            return regex.IsMatch(input);
        }

        public static bool IsIndexLine(string input, int lineNumber)
        {
            string indexPattern = @"^\d+$";

            if (Regex.IsMatch(input, indexPattern))
            {
                return true;
            }

            if (IsDivisibleBy4(lineNumber) && OnlyChineseNumber(input))
            {
                return true;
            }

            return false;
        }

        public static bool IsValidSrtFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return false;
            }

            string[] lines = File.ReadAllLines(filePath);

            // Regular expressions to match SRT format
            string timeStampPattern = @"^\d{2}:\d{2}:\d{2},\d{3} --> \d{2}:\d{2}:\d{2},\d{3}$";

            int i = 0;
            while (i < lines.Length)
            {
                // Check if line is an index
                if (!IsIndexLine(lines[i], i))
                {
                    return false;
                }

                i++;

                // Check if line is a timestamp
                if (i >= lines.Length || !Regex.IsMatch(lines[i], timeStampPattern))
                {
                    return false;
                }

                i++;

                // Skip lines of subtitle text
                while (i < lines.Length && !string.IsNullOrWhiteSpace(lines[i]))
                {
                    i++;
                }

                // Skip the blank line between subtitles
                if (i < lines.Length && string.IsNullOrWhiteSpace(lines[i]))
                {
                    i++;
                }
            }

            return true;
        }

        public static void MergeSrtFiles(string srtFile1, string srtFile2, string outputFile)
        {
            if (!File.Exists(srtFile1) || !File.Exists(srtFile2))
            {
                throw new FileNotFoundException("One or both input SRT files do not exist.");
            }

            string[] srtLines1 = File.ReadAllLines(srtFile1);
            string[] srtLines2 = File.ReadAllLines(srtFile2);

            if (srtLines1.Length != srtLines2.Length)
            {
                throw new InvalidOperationException("The SRT files do not have the same number of lines.");
            }

            using (StreamWriter writer = new StreamWriter(outputFile, false, Encoding.UTF8))
            {
                int i = 0;
                int tmp_start = 0;

                while (i < srtLines1.Length)
                {
                    // Write index
                    writer.WriteLine(srtLines1[i]);
                    i++;

                    // Write timestamp
                    writer.WriteLine(srtLines1[i]);
                    i++;

                    // remember where lyric start
                    tmp_start = i;

                    // Write first language subtitle
                    while (i < srtLines1.Length && !string.IsNullOrWhiteSpace(srtLines1[i]))
                    {
                        writer.WriteLine(srtLines1[i]);
                        i++;
                    }

                    // Write second language subtitle (from the second file)
                    int j = tmp_start;
                    while (j < srtLines2.Length && !string.IsNullOrWhiteSpace(srtLines2[j]))
                    {
                        writer.WriteLine(srtLines2[j]);
                        j++;
                    }

                    // Skip blank line in both files
                    writer.WriteLine();
                    i++;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string output_f = AppendSuffixToFileName(Lbx_SRT1.Items[0].ToString());
            MergeSrtFiles(Lbx_SRT1.Items[0].ToString(), Lbx_SRT2.Items[0].ToString(), output_f);

            DisplayFirst20Lines(output_f, Lbx_Output);
        }

        public static void DisplayFirst20Lines(string filePath, ListBox listBox)
        {
            if (!File.Exists(filePath))
            {
                MessageBox.Show("The specified file does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            listBox.Items.Clear();

            try
            {
                string[] lines = File.ReadAllLines(filePath);

                for (int i = 0; i < Math.Min(20, lines.Length); i++)
                {
                    listBox.Items.Add(lines[i]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while reading the file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_Clear1_Click(object sender, EventArgs e)
        {
            Lbx_SRT1.Items.Clear();
            Lbx_Output.Items.Clear();
            Btn_Combine.Enabled = false;
        }

        private void Btn_Clear2_Click(object sender, EventArgs e)
        {
            Lbx_SRT2.Items.Clear();
            Lbx_Output.Items.Clear();
            Btn_Combine.Enabled = false;
        }

        public static int GetFileLineCount(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("The specified file does not exist.");
            }

            int lineCount = 0;

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    while (reader.ReadLine() != null)
                    {
                        lineCount++;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while reading the file: {ex.Message}");
                throw;
            }

            return lineCount;
        }

        public static bool SrtLinesEqual(string s1, string s2)
        {
            if (GetFileLineCount(s1) == GetFileLineCount(s2))
            {
                return true;
            }

            return false;
        }

        public static string AppendSuffixToFileName(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("The file path cannot be null or empty.");
            }

            string directory = Path.GetDirectoryName(filePath);  // 取得路徑
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);  // 取得檔名（不含副檔名）
            string extension = Path.GetExtension(filePath);  // 取得副檔名

            string newFileName = $"{fileNameWithoutExtension}_2in1{extension}";  // 生成新檔名
            string newFilePath = Path.Combine(directory, newFileName);  // 生成新路徑

            return newFilePath;
        }

        private void HandleDragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }

        // add SRT verify here
        // if two SRT are loaded, then try to check num of lines are equal or not
        // -> NOT, disable button
        // -> enable

        // Button method
        // locate the pure number line which is following with timecode = Sequence
        // copy from the line of sequence + 2 to the line before next Sequence
    }
}
