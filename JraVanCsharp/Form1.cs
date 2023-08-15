using System.Diagnostics;
using static JVData_Struct;

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
            try
            {
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
                        JV_RA_RACE RaceInfo = new JV_RA_RACE();

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
                                        RaceInfo.SetDataB(ref strBuff);
                                        // �f�[�^�\��
                                        rtbData.AppendText(
                                            "�N:" + RaceInfo.id.Year
                                            + " ����:" + RaceInfo.id.MonthDay
                                            + " ��:" + clsCodeConv.getCodeName("2001", RaceInfo.id.JyoCD, 3)
                                            + " ��:" + RaceInfo.id.Kaiji
                                            + " ����:" + RaceInfo.id.Nichiji
                                            + " �q:" + RaceInfo.id.RaceNum
                                            + " ���[�X��:" + RaceInfo.RaceInfo.Ryakusyo10 + "\n");
                                    }
                                    else
                                    {
                                        // ���[�X�ڍ׈ȊO�͓ǂݔ�΂�
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
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return;
            }

            // JVLink �I������
            lReturnCode = axJVLink1.JVClose();
            if (lReturnCode != 0)
            {
                MessageBox.Show("JVClose �G���[�F" + lReturnCode);
            }
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
