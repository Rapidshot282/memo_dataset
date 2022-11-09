using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace memo_dataset
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static DataTable memoTable;
        private void Form1_Load(object sender, EventArgs e)
        {
            memO_TABLETableAdapter1.Fill(dataSet11.MEMO_TABLE);
            memoTable = dataSet11.Tables["MEMO_TABLE"];
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //memoTable의 Row를 불러와 리스트 박스에 출력
            listBox1.Items.Clear();

            foreach (DataRow row in memoTable.Rows)
            {
                listBox1.Items.Add(row["M_ID"].ToString());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string selectedMemo = "";

            DataRow[] selected;

            if (listBox1.SelectedItem == null)
            { //선택된 메모가 없을 시 예외처리
                MessageBox.Show("메모를 선택하세요.");
                return;
            }

            selectedMemo = listBox1.SelectedItem.ToString();
            //Select를 사용하여 메모 검색
            selected = memoTable.Select("M_ID = " + selectedMemo);

            foreach (DataRow row in selected)
            {
                //선택된 메모 내용 출력
                label2.Text = row["M_KEYWORD"].ToString();
                label3.Text = row["M_CONTENTS"].ToString();
                label4.Text = row["M_DATE"].ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataRow newRow = memoTable.NewRow(); //새로운 dataRow 생성


            //시퀀스 넘버 불러오기
            //SELECT memo_seq.nextval FROM DUAL
            newRow["M_ID"] = (int)memO_TABLETableAdapter1.getSeqVal();
            newRow["M_KEYWORD"] = textBox1.Text;
            newRow["M_DATE"] = (DateTime.Today).ToString();
            newRow["M_CONTENTS"] = richTextBox1.Text;

            memoTable.Rows.Add(newRow);
            //오라클 DB 테이블 업데이트 
            memO_TABLETableAdapter1.Update(dataSet11.MEMO_TABLE);

            MessageBox.Show("메모가 저장되었습니다.");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            richTextBox1.Text = "";
        }
    }
}
