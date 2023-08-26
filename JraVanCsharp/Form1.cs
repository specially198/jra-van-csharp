using System.Data;
using static JVData_Struct;
using Application = System.Windows.Forms.Application;

namespace JraVanCsharp
{
    public partial class frmMain : Form
    {
        // JVOpen:���_�E�����[�h�t�@�C����
        private int lDownloadCount;
        // �R�[�h�ϊ��C���X�^���X
        private ClsCodeConv clsCodeConv = null!;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // �����ݒ�
            string sid = "Test";

            // JVLink ������
            int lReturnCode = axJVLink1.JVInit(sid);

            // �G���[����
            if (lReturnCode != 0)
            {
                MessageBox.Show("JVInit �G���[ �R�[�h�F" + lReturnCode);
            }

            // �R�[�h�ϊ��C���X�^���X����
            clsCodeConv = new ClsCodeConv(Application.StartupPath + "/CodeTable.csv");
        }

        private void mnuConfJV_Click(object sender, EventArgs e)
        {
            // �ݒ��ʕ\��
            long lReturnCode = axJVLink1.JVSetUIProperties();
            if (lReturnCode != 0)
            {
                MessageBox.Show("JVSetUIProperties�G���[ �R�[�h�F" + lReturnCode);
            }
        }

        private void btnGetJVData_Click(object sender, EventArgs e)
        {
            int lReturnCode;
            DataSet jvdds = new DataSet("JvDatas");
            try
            {
                // �\���E�B���h�E�̏�����
                rtbData.Clear();

                // �i���\�������ݒ�
                // �^�C�}�[��~
                tmrDownload.Enabled = false;
                // JVData �_�E�����[�h�i��
                prgDownload.Value = 0;
                // JVData �ǂݍ��ݐi��
                prgJVRead.Value = 0;

                // �����ݒ�
                // JVOpen:�t�@�C�����ʎq
                string strDataSpec = "RACE";
                // JVOpen:�f�[�^�񋟓��t
                string strFromTime = "20050301000000";
                // JVOpen:�I�v�V����
                int lOption = 2;

                // JVLink �߂�l
                int lReadCount = 0;
                // JVOpen: �ŐV�t�@�C���̃^�C���X�^���v
                string strLastFileTimestamp;

                // JVLink �_�E�����[�h����
                lReturnCode = axJVLink1.JVOpen(
                    strDataSpec, strFromTime, lOption, ref lReadCount, ref lDownloadCount, out strLastFileTimestamp);

                // �G���[����
                if (lReturnCode != 0)
                {
                    MessageBox.Show("JVOpen �G���[�F" + lReturnCode);
                }
                else
                {
                    MessageBox.Show(
                        "�߂�l : " + lReturnCode + "\n"
                        + "�ǂݍ��݃t�@�C���� : " + lReadCount + "\n"
                        + "�_�E�����[�h�t�@�C���� : " + lDownloadCount + "\n"
                        + "�^�C���X�^���v : " + strLastFileTimestamp);

                    // �i���\���v���O���X�o�[�ő�l�ݒ�
                    if (lDownloadCount == 0)
                    {
                        // �_�E�����[�h�K�v����
                        prgDownload.Maximum = 100;
                        prgDownload.Value = 100;
                    }
                    else
                    {
                        prgDownload.Maximum = lDownloadCount;
                        // �^�C�}�[�J�n
                        tmrDownload.Enabled = true;
                    }
                    prgJVRead.Maximum = lReadCount;

                    if (lReadCount > 0)
                    {
                        // JVRead: �f�[�^�i�[�o�b�t�@�T�C�Y
                        int lBuffSize = 110000;
                        // JVRead: �f�[�^�i�[�o�b�t�@
                        string strBuff;
                        // JVRead: �_�E�����[�h�t�@�C����
                        string strFileName;
                        // ���[�X�ڍ׏��\����
                        JV_RA_RACE raceInfo = new JV_RA_RACE();
                        // �n�����[�X���\����
                        JV_SE_RACE_UMA raceUmaInfo = new JV_SE_RACE_UMA();

                        while (true)
                        {
                            // �o�b�N�O���E���h�ł̏��������s
                            Application.DoEvents();

                            // JVRead ��1�s�ǂݍ���
                            lReturnCode = axJVLink1.JVRead(out strBuff, out lBuffSize, out strFileName);
                            // ���^�[���R�[�h�ɂ�菈���𕪎}
                            switch (lReturnCode)
                            {
                                // �S�t�@�C���ǂݍ��ݏI��
                                case 0:
                                    // �i���\��
                                    prgJVRead.Value = prgJVRead.Maximum;
                                    goto readFinish;
                                // �t�@�C���؂�ւ��
                                case -1:
                                    prgJVRead.Value = prgJVRead.Value + 1;
                                    continue;
                                // �_�E�����[�h��
                                case -3:
                                    continue;
                                // Init ����ĂȂ�
                                case -201:
                                    MessageBox.Show("JVInit ���s���Ă��܂���B");
                                    goto readFinish;
                                // Open ����ĂȂ�
                                case -203:
                                    MessageBox.Show("JVOpen ���s���Ă��܂���B");
                                    goto readFinish;
                                // �t�@�C�����Ȃ�
                                case -503:
                                    MessageBox.Show(strFileName + "�����݂��܂���B");
                                    goto readFinish;
                                // ����ǂݍ���
                                case int i when i > 0:
                                    // ���R�[�h��� ID �̎���
                                    if (strBuff.Substring(0, 2) == "RA")
                                    {
                                        // ���[�X�ڍׂ̂ݏ���

                                        // ���[�X�ڍ׍\���̂ւ̓W�J
                                        raceInfo.SetDataB(ref strBuff);

                                        // �ǂݍ��񂾏����f�[�^�Z�b�g�֊i�[����
                                        AdoUtility.SetJVDataRaceDataSet(raceInfo, ref jvdds);
                                    }
                                    else if (strBuff.Substring(0, 2) == "SE")
                                    {
                                        // �n�����[�X���\���̂ւ̓W�J
                                        raceUmaInfo.SetDataB(ref strBuff);

                                        // �ǂݍ��񂾏����f�[�^�Z�b�g�֊i�[����
                                        AdoUtility.SetJVDataUmaRaceDataSet(raceUmaInfo, ref jvdds);
                                    }
                                    else
                                    {
                                        // ���[�X�ڍׁA�n�����[�X���ȊO�͓ǂݔ�΂�
                                        axJVLink1.JVSkip();
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                    readFinish:;
                    }

                    // �^�C�}�L�����́A����������
                    if (tmrDownload.Enabled)
                    {
                        tmrDownload.Enabled = false;
                        prgDownload.Value = prgDownload.Maximum;
                    }
                }

                // �f�[�^�Z�b�g�Ɋi�[�����e������ʂɕ\��
                DataTable dtRace = jvdds.Tables["Race"]!;

                // ���[�X�ڍׂ̌������擾
                var raceCount = dtRace.Rows.Count;

                // ���[�X�ڍׂ����݂��Ȃ��ꍇ
                if (raceCount == 0)
                {
                    MessageBox.Show("���[�X��񂪑��݂��܂���");
                }
                else
                {
                    // ���[�X�ڍׂ̑�10���[�X������(�����s)
                    var races = dtRace.AsEnumerable()
                        .Where(r => r.Field<string>("RaceNum") == "10")
                        .ToList();
                    if (races.Count == 0)
                    {
                        MessageBox.Show("�������ʂ�0���ł�");
                        return;
                    }

                    // �擾���ʂ̈ꌏ�ڂ��\���̂ɃZ�b�g
                    JV_RA_RACE raceInfo = new JV_RA_RACE();
                    AdoUtility.SetJVDataRaceStructure(races[0], ref raceInfo);

                    // ��ʕ\��
                    rtbData.AppendText(
                        "�N:" + raceInfo.id.Year
                        + " ����:" + raceInfo.id.MonthDay
                        + " ��:" + clsCodeConv.GetCodeName("2001", raceInfo.id.JyoCD, 3)
                        + " ��:" + raceInfo.id.Kaiji
                        + " ����:" + raceInfo.id.Nichiji
                        + " �q:" + raceInfo.id.RaceNum
                        + " ���[�X��:" + raceInfo.RaceInfo.Ryakusyo10 + "\n");

                    // �\���������[�X�ڍׂ̔n�����[�X����S�n���\��
                    // �f�[�^�Z�b�g���烌�[�X�ڍׂ̃L�[�Ŕn�����[�X���̍s(�����s)������
                    var raceUmas = jvdds.Tables["RaceUma"]!.AsEnumerable()
                        .Where(r => r.Field<string>("Year") == raceInfo.id.Year)
                        .Where(r => r.Field<string>("MonthDay") == raceInfo.id.MonthDay)
                        .Where(r => r.Field<string>("JyoCD") == raceInfo.id.JyoCD)
                        .Where(r => r.Field<string>("Kaiji") == raceInfo.id.Kaiji)
                        .Where(r => r.Field<string>("Nichiji") == raceInfo.id.Nichiji)
                        .Where(r => r.Field<string>("RaceNum") == raceInfo.id.RaceNum)
                        .ToList();

                    // ��s�����o���A��ʂɕ\��
                    foreach (var raceUma in raceUmas)
                    {
                        // �s�̏����\���̂ɃZ�b�g
                        JV_SE_RACE_UMA raceUmaInfo = new JV_SE_RACE_UMA();
                        AdoUtility.SetJVDataUmaRaceStructure(raceUma, ref raceUmaInfo);
                        // ��ʕ\��
                        rtbData.AppendText(
                            "�g:" + raceUmaInfo.Wakuban
                            + " �n��:" + raceUmaInfo.Umaban
                            + " �n��:" + raceUmaInfo.Bamei
                            + " �R��:" + raceUmaInfo.KisyuRyakusyo + "\n");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                // JVLink �I������
                lReturnCode = axJVLink1.JVClose();
                if (lReturnCode != 0)
                {
                    MessageBox.Show("JVClose �G���[�F" + lReturnCode);
                }
            }

            // �㏈��
            // JV-Data�f�[�^�Z�b�g�̃��\�[�X���
            jvdds.Dispose();
        }

        private void tmrDownload_Tick(object sender, EventArgs e)
        {
            // �_�E�����[�h�ς̃t�@�C������Ԃ�
            int lReturnCode = axJVLink1.JVStatus();
            // �G���[����
            if (lReturnCode < 0)
            {
                // �G���[

                MessageBox.Show("JVStatus�G���[:" + lReturnCode);
                // �^�C�}�[��~
                tmrDownload.Enabled = false;
                // JVLink�I������
                lReturnCode = axJVLink1.JVClose();
                if (lReturnCode != 0)
                {
                    MessageBox.Show("JVClose�G���[:" + lReturnCode);
                }
            }
            else if (lReturnCode < lDownloadCount)
            {
                // �_�E�����[�h��
                // �v���O���X�\��
                prgDownload.Value = lReturnCode;
            }
            else if (lReturnCode == lDownloadCount)
            {
                // �_�E�����[�h����
                // �^�C�}�[��~
                tmrDownload.Enabled = false;
                // �v���O���X�\��
                prgDownload.Value = lReturnCode;
            }
        }
    }
}
