using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RaceCarEstimator
{
    public partial class FormMain : Form
    {
        // Memebers
        private DataProcessor _dataProcessor;
        private List<RaceData> _raceDataList;
        public static Font _cellFont = new Font("Microsoft Sans Serif", 8F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
        public static Color selectedColor = Color.FromArgb(255, Color.DarkCyan);

        // Constructor
        public FormMain()
        {
            InitializeComponent();
            btnProcess.Cursor = Cursors.Hand;
            _dataProcessor = new DataProcessor();
            _raceDataList  = new List<RaceData>();
            SetStyleDataGridView(this.dgvOneCondition);
            SetStyleDataGridView(this.dgvTwoCondition);
            SetStyleDataGridView(this.dgvRaceChannels);
        }

        // Event handler for Process Button
        private void btnProcess_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog sfd = new OpenFileDialog()
            {
                Filter = "Data files (*.dat)|*.dat",
                Title = "Open an Data File"
            })
            {

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    _raceDataList = _dataProcessor.ReadData(sfd.FileName);
                    UpdateChannelsData();
                }
            }
            UpdateChannelDataGridViewTab1();
            UpdateOneConditionTab2();
            UpdateTwoConditionTab3();
        }

        // UpdateChannelsData function that update channel7
        private void UpdateChannelsData()
        {
            foreach(RaceData raceData in _raceDataList)
            {
                if(raceData._Channel5._Value != 0 && raceData._Channel4._Value != 0)
                {
                    raceData._Channel7._Value = raceData._Channel5._Value - raceData._Channel4._Value;
                }
            }
        }

        // SetStyleDataGridView function that update style for datagridview's header
        private void SetStyleDataGridView(DataGridView dataGridView)
        {
            dataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView.EnableHeadersVisualStyles = false;
        }

        // UpdateChannelDataGridViewTab1 function that update the race channels datagridview in tab1
        private void UpdateChannelDataGridViewTab1()
        {
            this.dgvRaceChannels.Rows.Clear();
            foreach (RaceData raceData in _raceDataList) {
                AddDataGridViewRowData(this.dgvRaceChannels, raceData);
            }
        }

        // UpdateOneConditionTab2 function that update the one condition datagridview in tab2
                private void UpdateOneConditionTab2()
        {
            this.dgvOneCondition.Rows.Clear();
            foreach (RaceData raceData in _raceDataList)
            {
                if(raceData._Channel2._Value < -0.5 || raceData._Channel7._Value < 0)
                {
                    AddDataGridViewRowData(this.dgvOneCondition, raceData);
                }
            }
        }

        // UpdateTwoConditionTab3 function that update the two conditions datagridview in tab3
        private void UpdateTwoConditionTab3()
        {
            this.dgvTwoCondition.Rows.Clear();
            foreach (RaceData raceData in _raceDataList)
            {
                if (raceData._Channel2._Value < -0.5 && raceData._Channel7._Value < 0)
                {
                    AddDataGridViewRowData(this.dgvTwoCondition, raceData);
                }
            }
        }


        // AddDataGridViewRowData function that add datagridview row in datagridview
        private void AddDataGridViewRowData(DataGridView dgv, RaceData raceData)
        {
            DataGridViewRow row = new DataGridViewRow();
            row.Cells.Add(GetDataGridViewTextBoxCellByValue(raceData._Time, true));
            row.Cells.Add(GetDataGridViewTextBoxCellByValue(raceData._Channel1._Value, false));
            row.Cells.Add(GetDataGridViewTextBoxCellByValue(raceData._Channel2._Value, false));
            row.Cells.Add(GetDataGridViewTextBoxCellByValue(raceData._Channel3._Value, false));
            row.Cells.Add(GetDataGridViewTextBoxCellByValue(raceData._Channel4._Value, false));
            row.Cells.Add(GetDataGridViewTextBoxCellByValue(raceData._Channel5._Value, false));
            row.Cells.Add(GetDataGridViewTextBoxCellByValue(raceData._Channel6._Value, false));
            row.Cells.Add(GetDataGridViewTextBoxCellByValue(raceData._Channel7._Value, false));
            dgv.Rows.Add(row);
        }

        // GetDataGridViewTextBoxCellByValue function that get the datagridview text cell with vaule
        private DataGridViewTextBoxCell GetDataGridViewTextBoxCellByValue(double val, bool isText)
        {
            DataGridViewTextBoxCell cell;
            cell = new DataGridViewTextBoxCell() { Value = val };
            cell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            cell.Style.Font = _cellFont;
            cell.Style.BackColor = Color.Black;
            cell.Style.ForeColor = Color.Yellow;
            cell.Style.SelectionBackColor = selectedColor;
            cell.Style.SelectionForeColor = Color.Yellow;
            cell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            if (!isText) {
                if (val > 0)
                {
                    cell.Style.BackColor = Color.DarkGreen;
                    cell.Style.ForeColor = Color.Yellow;
                }
                else if (val < 0)
                {
                    cell.Style.ForeColor = Color.Yellow;
                    cell.Style.BackColor = Color.DarkRed;
                }
                else
                {
                    cell.Style.ForeColor = Color.Yellow;
                    cell.Style.BackColor = Color.DarkSlateGray;
                }
            }
            return cell;
        }
    }
}
