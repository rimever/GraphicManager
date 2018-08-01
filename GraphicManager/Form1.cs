using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace GraphicManager
{
    public partial class Form1 : Form
    {
        Bitmap baseBitmap;
        public Form1()
        {
            InitializeComponent();
            //QQQ:�����̏����摜�͂ǂ����邩�ˁH
            LoadGraphic("bit.jpg");
        }
        /// <summary>
        /// �摜���Ăяo��
        /// </summary>
        /// <param name="filename"></param>
        private void LoadGraphic(String filename)
        {
            baseBitmap = new Bitmap(filename);
            pictureBox1.Size = baseBitmap.Size;
            this.Width = pictureBox1.Width;
            // �����傫�߂Ɏ���������ǂ�
            this.Height = pictureBox1.Top + pictureBox1.Height + 20;
            ResetDrawOptionCheck();
            �ʏ�`��ToolStripMenuItem.Checked = true;
            DrawNormal();
            //���ݑI�𒆂̕\���`���ŕ\��
            /*
            if (�ʏ�`��ToolStripMenuItem.Checked)
            {
                DrawNormal();
            }
            if (�l�K���]ToolStripMenuItem.Checked)
            {
                DrawNegative();
            }
            if (���U�C�NToolStripMenuItem.Checked)
            {
                DrawMosaic();
            }
            */
        }
        private static Bitmap BmpNegative(String input)
        {
            Bitmap bitmap = new Bitmap(input);

            for (int y = 0; y < bitmap.PhysicalDimension.Height; y++)
            {
                for (int x = 0; x < bitmap.PhysicalDimension.Width; x++)
                {

                    Color c = bitmap.GetPixel(x, y);
                    bitmap.SetPixel(x, y, Color.FromArgb((byte)~c.R, (byte)~c.G, (byte)~c.B));
                }
            }
            return bitmap;
        }
        /// <summary>
        /// ���̃\�[�X�𗘗p���₷���`��
        /// </summary>
        /// <param name="base_bit"></param>
        /// <returns></returns>
        public Bitmap Mosaic(Bitmap base_bit)
        {
            Bitmap return_bit;
            Mosaic(out return_bit, base_bit, 10);
            return return_bit;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bitmapDataSrc"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        unsafe public static Color CalcMosaic(BitmapData bitmapDataSrc, int width, int height, int x, int y, int size)
        {
            Color color;
            int r = 0, g = 0, b = 0, count = 0;
            double[] Mosaic = new double[3] { 0, 0, 0 };
            uint* src = (uint*)(void*)bitmapDataSrc.Scan0;
            for (int m = 0; m < size; m++)
            {
                int i = y + m; int ySrc = i * bitmapDataSrc.Stride / sizeof(uint); if (i < 0 || i >= height) continue;
                for (int n = 0; n < size; n++)
                {
                    int j = x + n;
                    if (j < 0 || j >= width) continue;
                    color = Color.FromArgb((int)src[ySrc + j]);
                    r += (int)color.R;
                    g += (int)color.G;
                    b += (int)color.B;
                    count++;
                }
            }
            color = Color.FromArgb((byte)(r / count), (byte)(g / count), (byte)(b / count)); return color;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bitmapdst"></param>
        /// <param name="bitmapsrc"></param>
        /// <param name="size"></param>
        public void Mosaic(out Bitmap bitmapdst, Bitmap bitmapsrc, int size)
        {
            //�Z�[�t���[�h�ȊO�̃R�[�h�u���b�N�̋���true�ɂ���K�v������܂�
            bitmapdst = new Bitmap(bitmapsrc.Width, bitmapsrc.Height); 
            Rectangle rectangle = new Rectangle(0, 0, bitmapsrc.Width, bitmapsrc.Height); 
            BitmapData bitmapDataSrc = bitmapsrc.LockBits(rectangle, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb); 
            BitmapData bitmapDataDst = bitmapdst.LockBits(rectangle, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            unsafe
            {
                // �r�b�g�}�b�v�̍ŏ��̃f�[�^�ʒu
                uint* dst = (uint*)(void*)bitmapDataDst.Scan0; 
                for (int y = 0; y < bitmapsrc.Height; y += size)
                {
                    this.Text = "�����F" + (100 * y / bitmapDataSrc.Height) + "%";
                    for (int x = 0; x < bitmapsrc.Width; x += size)
                    {
                        Color mosaic = CalcMosaic(bitmapDataSrc, bitmapsrc.Width, bitmapsrc.Height, x, y, size);
                        for (int m = 0; m < size; m++)
                        {
                            int i = y + m; 
                            int yDst = i * bitmapDataDst.Stride / sizeof(uint);
                            if (i < 0 || i >= bitmapsrc.Height) continue;
                            for (int n = 0; n < size; n++)
                            {
                                int j = x + n; 
                                if (j < 0 || j >= bitmapsrc.Width) continue;
                                dst[yDst + j] = (uint)mosaic.ToArgb();
                            }
                        }
                    }
                }
            }
            bitmapdst.UnlockBits(bitmapDataDst); 
            bitmapsrc.UnlockBits(bitmapDataSrc);
        }
        /// <summary>
        /// �l�K���]�B���U�C�N�̏������Q�l�ɍ쐬
        /// </summary>
        /// <param name="bitmapsrc"></param>
        /// <returns></returns>
        public Bitmap Negative(Bitmap bitmapsrc)
        {
            Bitmap bitmapdst = new Bitmap(bitmapsrc.Width, bitmapsrc.Height);
            Rectangle rectangle = new Rectangle(0, 0, bitmapsrc.Width, bitmapsrc.Height); 
            BitmapData bitmapDataSrc = bitmapsrc.LockBits(rectangle, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb); 
            BitmapData bitmapDataDst = bitmapdst.LockBits(rectangle, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            unsafe
            {
                uint* src = (uint*)(void*)bitmapDataSrc.Scan0;
                uint* dst = (uint*)(void*)bitmapDataDst.Scan0;

                for (int y = 0; y < bitmapsrc.Height; y++)
                {
                    this.Text = "�����F" + (100 * y / bitmapDataSrc.Height) + "%";
                    int ySrc = y * bitmapDataSrc.Stride / sizeof(uint);
                    int yDst = y * bitmapDataDst.Stride / sizeof(uint);
                    for (int x = 0; x < bitmapsrc.Width; x++)
                    {
                        Color color = Color.FromArgb((int)src[ySrc + x]);
                        Color change = Color.FromArgb((byte)~color.R, (byte)~color.G, (byte)~color.B);
                        dst[yDst + x] = (uint)change.ToArgb();
                    }
                }
            }
            bitmapdst.UnlockBits(bitmapDataDst);
            bitmapsrc.UnlockBits(bitmapDataSrc);
            return bitmapdst;
        }

        private void �J��ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }
        private void �ۑ�ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        private void �ʏ�`��ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (�ʏ�`��ToolStripMenuItem.Checked) { return; }
            //�@�`�F�b�N������
            ResetDrawOptionCheck();
            �ʏ�`��ToolStripMenuItem.Checked = true;
            //
            DrawNormal();
        }
        private void DrawNormal()
        {
            pictureBox1.Image = baseBitmap;
        }
        /// <summary>
        /// �`��I�v�V�����̃`�F�b�N�����Z�b�g
        /// </summary>
        private void ResetDrawOptionCheck()
        {
            �ʏ�`��ToolStripMenuItem.Checked = false;
            �l�K���]ToolStripMenuItem.Checked = false;
            ���U�C�NToolStripMenuItem.Checked = false;
            �O���[�X�P�[��ToolStripMenuItem.Checked = false;
            �G�b�W���oToolStripMenuItem.Checked = false;
            �ʐ^ToolStripMenuItem.Checked = false;
            �Z�s�A��ToolStripMenuItem.Checked = false;
            �m�C�Y����ToolStripMenuItem.Checked = false;
            �G�b�W���o����ToolStripMenuItem.Checked = false;
            �א���ToolStripMenuItem.Checked = false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void �l�K���]ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (�l�K���]ToolStripMenuItem.Checked) { return; }
            //�@�`�F�b�N������
            ResetDrawOptionCheck();
            �l�K���]ToolStripMenuItem.Checked = true;
            // �����J�n
            DrawNegative();
        }
        private void DrawNegative()
        {
            DateTime start = DateTime.Now;
            this.Text = "������";
            pictureBox1.Image = Negative(baseBitmap);
            DateTime end = DateTime.Now;
            this.Text = "�����I���F" + (end - start);

        }

        private void ���U�C�NToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (���U�C�NToolStripMenuItem.Checked) { return; }
            //
            ResetDrawOptionCheck();
            ���U�C�NToolStripMenuItem.Checked = true;
            //
            DrawMosaic();
        }
        private void DrawMosaic()
        {
            DateTime start = DateTime.Now;
            this.Text = "������";
            pictureBox1.Image = Mosaic(baseBitmap);
            DateTime end = DateTime.Now;
            this.Text = "�����I���F" + (end - start);
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            pictureBox1.Image.Save(saveFileDialog1.FileName);
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            LoadGraphic(openFileDialog1.FileName);
        }

        private void ���̂𒊏oToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (���̂𒊏oToolStripMenuItem.Checked) { return; }
            ResetDrawOptionCheck();
            //���̂𒊏oToolStripMenuItem.Checked = true;
            //
            DateTime start = DateTime.Now;
            this.Text = "������";
            pictureBox1.Image = ExtractObject(baseBitmap);
            DateTime end = DateTime.Now;
            this.Text = "�����I���F" + (end - start);
        }
        /// <summary>
        /// ����͎��s��
        /// </summary>
        /// <param name="bitmapsrc"></param>
        /// <returns></returns>
        private Bitmap ExtractObject(Bitmap bitmapsrc)
        {
            Bitmap bitmapdst = new Bitmap(bitmapsrc.Width, bitmapsrc.Height);
            Rectangle rectangle = new Rectangle(0, 0, bitmapsrc.Width, bitmapsrc.Height);
            BitmapData bitmapDataSrc = bitmapsrc.LockBits(rectangle, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData bitmapDataDst = bitmapdst.LockBits(rectangle, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            unsafe
            {
                uint* src = (uint*)(void*)bitmapDataSrc.Scan0;
                uint* dst = (uint*)(void*)bitmapDataDst.Scan0;

                for (int y = 0; y < bitmapsrc.Height; y++)
                {
                    this.Text = "����" + (100 * y / bitmapDataSrc.Height) + "%";
                    int ySrc = y * bitmapDataSrc.Stride / sizeof(uint);
                    int yDst = y * bitmapDataDst.Stride / sizeof(uint);
                    for (int x = 0; x < bitmapsrc.Width; x++)
                    {

                        Color color = Color.FromArgb((int)src[ySrc + x]);
                        Color change;
                        //if (color.GetBrightness() < 85f / 256f)
                        if ((color.R+color.G+color.B)/3 > 85)
                        {
                            change = Color.White;
                        }
                        else
                        {
                            change = Color.Black;
                        }
                         //Color.FromArgb((byte)~color.R, (byte)~color.G, (byte)~color.B);
                        dst[yDst + x] = (uint)change.ToArgb();
                        //this.Text = "" + color.GetBrightness();
                    }
                }
            }
            bitmapdst.UnlockBits(bitmapDataDst);
            bitmapsrc.UnlockBits(bitmapDataSrc);
            return bitmapdst;
        }

        private void �O���[�X�P�[��ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (�O���[�X�P�[��ToolStripMenuItem.Checked)
            {
                return;
            }
            ResetDrawOptionCheck();
            �O���[�X�P�[��ToolStripMenuItem.Checked = true;
            DateTime start = DateTime.Now;
            this.Text = "������";
            pictureBox1.Image = GrayScale(baseBitmap);
            DateTime end = DateTime.Now;
            this.Text = "�����I���F" + (end - start);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bitmapsrc"></param>
        /// <returns></returns>
        private Bitmap GrayScale(Bitmap bitmapsrc)
        {
            return GrayScale(bitmapsrc, false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bitmapsrc"></param>
        /// <param name="sub">�T�u���[�`�������Ďg�p����̂�</param>
        /// <returns></returns>
        private Bitmap GrayScale(Bitmap bitmapsrc,bool sub)
        {
            Bitmap bitmapdst = new Bitmap(bitmapsrc.Width, bitmapsrc.Height);
            Rectangle rectangle = new Rectangle(0, 0, bitmapsrc.Width, bitmapsrc.Height);
            BitmapData bitmapDataSrc = bitmapsrc.LockBits(rectangle, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData bitmapDataDst = bitmapdst.LockBits(rectangle, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            unsafe
            {
                uint* src = (uint*)(void*)bitmapDataSrc.Scan0;
                uint* dst = (uint*)(void*)bitmapDataDst.Scan0;

                for (int y = 0; y < bitmapsrc.Height; y++)
                {
                    if (sub)
                    {
                        // �T�u���[�`���Ȃ�Δ����@���݃G�b�W���o�ŃT�u���[�`���Ƃ��Ďg���Ă���
                        this.Text = "�����F" + (100 * y / bitmapDataSrc.Height / 2) + "%";
                    }
                    else
                    {
                        this.Text = "�����F" + (100 * y / bitmapDataSrc.Height) + "%";
                    }
                    int ySrc = y * bitmapDataSrc.Stride / sizeof(uint);
                    int yDst = y * bitmapDataDst.Stride / sizeof(uint);
                    for (int x = 0; x < bitmapsrc.Width; x++)
                    {
                        //���ς�������
                        Color color = Color.FromArgb((int)src[ySrc + x]);
                        Color change = Color.FromArgb((color.R + color.G + color.B) / 3, 
                            (color.R + color.G + color.B) / 3,
                            (color.R + color.G + color.B) / 3);

                        //Color.FromArgb((byte)~color.R, (byte)~color.G, (byte)~color.B);
                        dst[yDst + x] = (uint)change.ToArgb();
                        //this.Text = "" + color.GetBrightness();
                    }
                }
            }
            bitmapdst.UnlockBits(bitmapDataDst);
            bitmapsrc.UnlockBits(bitmapDataSrc);
            return bitmapdst;
        }

        private void �G�b�W���oToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (�G�b�W���oToolStripMenuItem.Checked)
            {
                return;
            }
            ResetDrawOptionCheck();
            �G�b�W���oToolStripMenuItem.Checked = true;
            DateTime start = DateTime.Now;
            this.Text = "������";
            pictureBox1.Image =�@ExtractEdge(baseBitmap);//ExtractEdge(baseBitmap);
            DateTime end = DateTime.Now;
            this.Text = "�����I���F" + (end - start);
        }
        /// <summary>
        /// �ꎟ�����ɂ��G�b�W���o
        /// </summary>
        /// <param name="bitmapsrc"></param>
        /// <returns></returns>
        private Bitmap ExtractEdge(Bitmap bitmapsrc)
        {
            // �ςȐF��������̂ŃO���[�X�P�[����
            bitmapsrc = GrayScale(bitmapsrc, true);
            Bitmap bitmapdst = new Bitmap(bitmapsrc.Width, bitmapsrc.Height);
            Rectangle rectangle = new Rectangle(0, 0, bitmapsrc.Width, bitmapsrc.Height);
            BitmapData bitmapDataSrc = bitmapsrc.LockBits(rectangle, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData bitmapDataDst = bitmapdst.LockBits(rectangle, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            unsafe
            {
                uint* src = (uint*)(void*)bitmapDataSrc.Scan0;
                uint* dst = (uint*)(void*)bitmapDataDst.Scan0;

                for (int y = 1; y < bitmapsrc.Height-1; y++)
                {
                    // �O���[�X�P�[����p���Ă���̂łT�O������Z
                    this.Text = "�����F" + (50 + 100 * y / bitmapDataSrc.Height / 2) + "%";
                    //
                    int ySrc = y * bitmapDataSrc.Stride / sizeof(uint);
                    int yDst = y * bitmapDataDst.Stride / sizeof(uint);
                    for (int x = 1; x < bitmapsrc.Width - 1; x++)
                    {
                        int height_r, height_g, height_b;
                        int width_r, width_g, width_b;
                        height_b = height_g = height_r = 0;
                        width_b = width_g = width_r = 0;
                        for (int k = -1; k <= 1; k++)
                        {
                            for (int l = -1; l <= 1; l++)
                            {
                                Color color = Color.FromArgb((int)src[(y + k) * bitmapDataSrc.Stride / sizeof(uint) + x + l]);

                                //Prewitt�t�B���^
                                height_b += k * (int)color.B;
                                height_g += k * (int)color.G;
                                height_r += k * (int)color.R;
                                //
                                width_b += l * (int)color.B;
                                width_g += l * (int)color.G;
                                width_r += l * (int)color.R;

                                //Sobel�t�B���^
                                /*
                                height_b += (l == 0 ? 2 * k : k) * (int)color.B;
                                height_g += (l == 0 ? 2 * k : k) * (int)color.G;
                                height_r += (l == 0 ? 2 * k : k) * (int)color.R;
                                //
                                width_b += (k == 0 ? 2 * l : l) * (int)color.B;
                                width_g += (k == 0 ? 2 * l : l) * (int)color.G;
                                width_r += (k == 0 ? 2 * l : l) * (int)color.R;
                                */
                            }
                        }
                        Color change = Color.FromArgb((byte)Math.Sqrt(height_r * height_r + width_r * width_r),
                            (byte)Math.Sqrt(height_g * height_g + width_g * width_g),
                            (byte)Math.Sqrt(height_b * height_b + width_b * width_b));
                        //
                        int renew_r, renew_g, renew_b;
                        /*
                        if (((int)(change.R + change.G + change.B)) / 3 < 128)
                        {
                            renew_r = 256 * (int)(change.R + change.G + change.B) / (256 * 3);
                            renew_g = 256 * (int)(change.R + change.G + change.B) / (256 * 3);
                            renew_b = 256 * (int)(change.R + change.G + change.B) / (256 * 3);
                        }
                        else
                        {
                            renew_r = 0 * (int)(change.R + change.G + change.B) / (256 * 3);
                            renew_g = 0 * (int)(change.R + change.G + change.B) / (256 * 3);
                            renew_b = 256 * (int)(change.R + change.G + change.B) / (256 * 3);
                        }
                         */
                        /*
                        renew_r = 255 * (int)(change.R + change.G + change.B) / (256 * 3);
                        renew_g = 255/2 - 255/2 * (int)(change.R + change.G + change.B) / (256 * 3);
                        renew_b = 0 * (int)(change.R + change.G + change.B) / (256 * 3);
                        Color renew = Color.FromArgb((byte)renew_r, (byte)renew_g, (byte)renew_b);
                        dst[yDst + x] = (uint)renew.ToArgb();
                         */
                        dst[yDst + x] = (uint)change.ToArgb();
                    }
                }
            }
            bitmapdst.UnlockBits(bitmapDataDst);
            bitmapsrc.UnlockBits(bitmapDataSrc);
            return bitmapdst;
        }

        /// <summary>
        /// �Q�������ɂ��G�b�W���o
        /// </summary>
        /// <param name="bitmapsrc"></param>
        /// <returns></returns>
        private Bitmap ExtractEdgeLaplacian(Bitmap bitmapsrc)
        {
            Bitmap bitmapdst = new Bitmap(bitmapsrc.Width, bitmapsrc.Height);
            Rectangle rectangle = new Rectangle(0, 0, bitmapsrc.Width, bitmapsrc.Height);
            BitmapData bitmapDataSrc = bitmapsrc.LockBits(rectangle, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData bitmapDataDst = bitmapdst.LockBits(rectangle, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            unsafe
            {
                uint* src = (uint*)(void*)bitmapDataSrc.Scan0;
                uint* dst = (uint*)(void*)bitmapDataDst.Scan0;

                for (int y = 1; y < bitmapsrc.Height - 1; y++)
                {
                    int ySrc = y * bitmapDataSrc.Stride / sizeof(uint);
                    int yDst = y * bitmapDataDst.Stride / sizeof(uint);
                    for (int x = 1; x < bitmapsrc.Width - 1; x++)
                    {
                        int r, g, b;
                        r = g = b = 0;

                        for (int k = -1; k <= 1; k++)
                        {
                            for (int l = -1; l <= 1; l++)
                            {
                                int weight;
                                /*
                                if (k == 0 && l == 0)
                                {
                                    weight = -4;
                                }
                                else
                                {
                                    if (k == 0 || l == 0)
                                    {
                                        weight = 1;
                                    }
                                    else
                                    {
                                        weight = 0;
                                    }
                                }
                                 */
                                if (k == 0 && l == 0)
                                {
                                    weight = 8;
                                }
                                else
                                {
                                    weight = -1;
                                } 

                                Color color = Color.FromArgb((int)src[(y + k) * bitmapDataSrc.Stride / sizeof(uint) + x + l]);
                                r += weight * (int)color.R;
                                g += weight * (int)color.G;
                                b += weight * (int)color.B;
                            }
                        }

                        Color change = Color.FromArgb((byte)Math.Abs(r), (byte)Math.Abs(g), (byte)Math.Abs(b));
                        dst[yDst + x] = (uint)change.ToArgb();
                    }
                }
            }
            bitmapdst.UnlockBits(bitmapDataDst);
            bitmapsrc.UnlockBits(bitmapDataSrc);
            return bitmapdst;
        }

        private void �ʐ^ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (�ʐ^ToolStripMenuItem.Checked) { return; }
            ResetDrawOptionCheck();
            //
            DateTime start = DateTime.Now;
            this.Text = "������";
            �ʐ^ToolStripMenuItem.Checked = true;
            pictureBox1.Image = BluePicture(baseBitmap);
            DateTime end = DateTime.Now;
            this.Text = "�����I���F" + (end - start);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bitmapsrc"></param>
        /// <returns></returns>
        private Bitmap BluePicture(Bitmap bitmapsrc)
        {
            Bitmap bitmapdst = new Bitmap(bitmapsrc.Width, bitmapsrc.Height);
            Rectangle rectangle = new Rectangle(0, 0, bitmapsrc.Width, bitmapsrc.Height);
            BitmapData bitmapDataSrc = bitmapsrc.LockBits(rectangle, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData bitmapDataDst = bitmapdst.LockBits(rectangle, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            unsafe
            {
                uint* src = (uint*)(void*)bitmapDataSrc.Scan0;
                uint* dst = (uint*)(void*)bitmapDataDst.Scan0;

                for (int y = 0; y < bitmapsrc.Height; y++)
                {
                    this.Text = "�����F" + (100 * y / bitmapDataSrc.Height) + "%";
                    int ySrc = y * bitmapDataSrc.Stride / sizeof(uint);
                    int yDst = y * bitmapDataDst.Stride / sizeof(uint);
                    for (int x = 0; x < bitmapsrc.Width; x++)
                    {
                        //���ς�������
                        Color color = Color.FromArgb((int)src[ySrc + x]);
                        Color change = Color.FromArgb(0,
                            0,
                            (color.R + color.G + color.B) / 3);

                        dst[yDst + x] = (uint)change.ToArgb();
                    }
                }
            }
            bitmapdst.UnlockBits(bitmapDataDst);
            bitmapsrc.UnlockBits(bitmapDataSrc);
            return bitmapdst;
        }

        private void �Z�s�A��ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (�Z�s�A��ToolStripMenuItem.Checked) { return; }
            ResetDrawOptionCheck();
            //
            DateTime start = DateTime.Now;
            this.Text = "������";
            �Z�s�A��ToolStripMenuItem.Checked = true;
            pictureBox1.Image = SepiaPicture(baseBitmap);
            DateTime end = DateTime.Now;
            this.Text = "�����I���F" + (end - start);
        }
        private Bitmap SepiaPicture(Bitmap bitmapsrc)
        {
            Bitmap bitmapdst = new Bitmap(bitmapsrc.Width, bitmapsrc.Height);
            Rectangle rectangle = new Rectangle(0, 0, bitmapsrc.Width, bitmapsrc.Height);
            BitmapData bitmapDataSrc = bitmapsrc.LockBits(rectangle, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData bitmapDataDst = bitmapdst.LockBits(rectangle, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            unsafe
            {
                uint* src = (uint*)(void*)bitmapDataSrc.Scan0;
                uint* dst = (uint*)(void*)bitmapDataDst.Scan0;

                for (int y = 0; y < bitmapsrc.Height; y++)
                {
                    this.Text = "�����F" + (100 * y / bitmapDataSrc.Height) + "%";
                    int ySrc = y * bitmapDataSrc.Stride / sizeof(uint);
                    int yDst = y * bitmapDataDst.Stride / sizeof(uint);
                    for (int x = 0; x < bitmapsrc.Width; x++)
                    {
                        //���ς�������
                        Color color = Color.FromArgb((int)src[ySrc + x]);
                        Color change = Color.FromArgb((int)(color.GetBrightness() * 240f),
                            (int)(color.GetBrightness() * 200f),
                            (int)(color.GetBrightness() * 140f));

                        dst[yDst + x] = (uint)change.ToArgb();
                    }
                }
            }
            bitmapdst.UnlockBits(bitmapDataDst);
            bitmapsrc.UnlockBits(bitmapDataSrc);
            return bitmapdst;
        }
        /// <summary>
        /// Median�u���F�m�C�Y����
        /// </summary>
        /// <param name="bitmapsrc"></param>
        /// <returns></returns>
        private Bitmap MedianFilter(Bitmap bitmapsrc)
        {
            Bitmap bitmapdst = new Bitmap(bitmapsrc.Width, bitmapsrc.Height);
            Rectangle rectangle = new Rectangle(0, 0, bitmapsrc.Width, bitmapsrc.Height);
            BitmapData bitmapDataSrc = bitmapsrc.LockBits(rectangle, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData bitmapDataDst = bitmapdst.LockBits(rectangle, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            unsafe
            {
                uint* src = (uint*)(void*)bitmapDataSrc.Scan0;
                uint* dst = (uint*)(void*)bitmapDataDst.Scan0;

                for (int y = 1; y < bitmapsrc.Height - 1; y++)
                {
                    this.Text = "�����F" + (100 * y / bitmapDataSrc.Height) + "%";
                    int ySrc = y * bitmapDataSrc.Stride / sizeof(uint);
                    int yDst = y * bitmapDataDst.Stride / sizeof(uint);
                    for (int x = 1; x < bitmapsrc.Width - 1; x++)
                    {
                        List<int> filter = new List<int>();
                        for (int k = -1; k <= 1; k++)
                        {
                            for (int l = -1; l <= 1; l++)
                            {
                                filter.Add((int)src[(y + k) * bitmapDataSrc.Stride / sizeof(uint) + x + l]);
                            }
                        }
                        filter.Sort();
                        Color change = Color.FromArgb(filter[filter.Count / 2]);
                        dst[yDst + x] = (uint)change.ToArgb();
                    }
                }
            }
            bitmapdst.UnlockBits(bitmapDataDst);
            bitmapsrc.UnlockBits(bitmapDataSrc);
            return bitmapdst;
        }

        private void �m�C�Y����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (�m�C�Y����ToolStripMenuItem.Checked) { return; }
            ResetDrawOptionCheck();
            �m�C�Y����ToolStripMenuItem.Checked = true;
            //
            DateTime start = DateTime.Now;
            this.Text = "������";
            pictureBox1.Image = MedianFilter(baseBitmap);
            DateTime end = DateTime.Now;
            this.Text = "�����I���F" + (end - start);

        }

        private void �G�b�W���o����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (�G�b�W���o����ToolStripMenuItem.Checked) { return; }
            ResetDrawOptionCheck();
            �G�b�W���o����ToolStripMenuItem.Checked = true;
            //
            DateTime start = DateTime.Now;
            this.Text = "������";
            pictureBox1.Image = EdgeHilditch(baseBitmap);
            DateTime end = DateTime.Now;
            this.Text = "�����I���F" + (end - start);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bitmapsrc"></param>
        /// <returns></returns>
        private Bitmap ExtractEdgeBlack(Bitmap bitmapsrc)
        {
            // �ςȐF��������̂ŃO���[�X�P�[����
            bitmapsrc = GrayScale(bitmapsrc, true);
            Bitmap bitmapdst = new Bitmap(bitmapsrc.Width, bitmapsrc.Height);
            Rectangle rectangle = new Rectangle(0, 0, bitmapsrc.Width, bitmapsrc.Height);
            BitmapData bitmapDataSrc = bitmapsrc.LockBits(rectangle, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData bitmapDataDst = bitmapdst.LockBits(rectangle, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            unsafe
            {
                uint* src = (uint*)(void*)bitmapDataSrc.Scan0;
                uint* dst = (uint*)(void*)bitmapDataDst.Scan0;

                for (int y = 1; y < bitmapsrc.Height - 1; y++)
                {
                    // �O���[�X�P�[����p���Ă���̂łT�O������Z
                    this.Text = "�����F" + (50 + 100 * y / bitmapDataSrc.Height / 2) + "%";
                    //
                    int ySrc = y * bitmapDataSrc.Stride / sizeof(uint);
                    int yDst = y * bitmapDataDst.Stride / sizeof(uint);
                    for (int x = 1; x < bitmapsrc.Width - 1; x++)
                    {
                        int height_r, height_g, height_b;
                        int width_r, width_g, width_b;
                        height_b = height_g = height_r = 0;
                        width_b = width_g = width_r = 0;
                        for (int k = -1; k <= 1; k++)
                        {
                            for (int l = -1; l <= 1; l++)
                            {
                                Color color = Color.FromArgb((int)src[(y + k) * bitmapDataSrc.Stride / sizeof(uint) + x + l]);

                                //Prewitt�t�B���^
                                height_b += k * (int)color.B;
                                height_g += k * (int)color.G;
                                height_r += k * (int)color.R;
                                //
                                width_b += l * (int)color.B;
                                width_g += l * (int)color.G;
                                width_r += l * (int)color.R;

                                //Sobel�t�B���^
                                /*
                                height_b += (l == 0 ? 2 * k : k) * (int)color.B;
                                height_g += (l == 0 ? 2 * k : k) * (int)color.G;
                                height_r += (l == 0 ? 2 * k : k) * (int)color.R;
                                //
                                width_b += (k == 0 ? 2 * l : l) * (int)color.B;
                                width_g += (k == 0 ? 2 * l : l) * (int)color.G;
                                width_r += (k == 0 ? 2 * l : l) * (int)color.R;
                                */
                            }
                        }
                        // �����Ȃ̂Ńl�K���]
                        Color change = Color.FromArgb((byte)~(byte)Math.Sqrt(height_r * height_r + width_r * width_r),
                            (byte)~(byte)Math.Sqrt(height_g * height_g + width_g * width_g),
                            (byte)~(byte)Math.Sqrt(height_b * height_b + width_b * width_b));
                        dst[yDst + x] = (uint)change.ToArgb();

                    }
                }
            }
            bitmapdst.UnlockBits(bitmapDataDst);
            bitmapsrc.UnlockBits(bitmapDataSrc);
            return bitmapdst;
        }
        //
        private Bitmap ExtractEdgeHilditch(Bitmap bitmapsrc)
        {
            // �ςȐF��������̂ŃO���[�X�P�[����
            Bitmap bitmapdst = new Bitmap(bitmapsrc.Width, bitmapsrc.Height);
            Rectangle rectangle = new Rectangle(0, 0, bitmapsrc.Width, bitmapsrc.Height);
            BitmapData bitmapDataSrc = bitmapsrc.LockBits(rectangle, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData bitmapDataDst = bitmapdst.LockBits(rectangle, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            unsafe
            {
                uint* src = (uint*)(void*)bitmapDataSrc.Scan0;
                uint* dst = (uint*)(void*)bitmapDataDst.Scan0;

                for (int y = 1; y < bitmapsrc.Height - 1; y++)
                {
                    // �O���[�X�P�[����p���Ă���̂łT�O������Z
                    this.Text = "�����F" + (50 * y / bitmapDataSrc.Height) + "%";
                    //
                    int ySrc = y * bitmapDataSrc.Stride / sizeof(uint);
                    int yDst = y * bitmapDataDst.Stride / sizeof(uint);
                    for (int x = 1; x < bitmapsrc.Width - 1; x++)
                    {
                        int height_r, height_g, height_b;
                        int width_r, width_g, width_b;
                        height_b = height_g = height_r = 0;
                        width_b = width_g = width_r = 0;
                        for (int k = -1; k <= 1; k++)
                        {
                            for (int l = -1; l <= 1; l++)
                            {
                                Color color = Color.FromArgb((int)src[(y + k) * bitmapDataSrc.Stride / sizeof(uint) + x + l]);

                                //Prewitt�t�B���^
                                height_b += k * (int)color.B;
                                height_g += k * (int)color.G;
                                height_r += k * (int)color.R;
                                //
                                width_b += l * (int)color.B;
                                width_g += l * (int)color.G;
                                width_r += l * (int)color.R;

                                //Sobel�t�B���^
                                /*
                                height_b += (l == 0 ? 2 * k : k) * (int)color.B;
                                height_g += (l == 0 ? 2 * k : k) * (int)color.G;
                                height_r += (l == 0 ? 2 * k : k) * (int)color.R;
                                //
                                width_b += (k == 0 ? 2 * l : l) * (int)color.B;
                                width_g += (k == 0 ? 2 * l : l) * (int)color.G;
                                width_r += (k == 0 ? 2 * l : l) * (int)color.R;
                                */
                            }
                        }
                        Color change = Color.FromArgb((byte)Math.Sqrt(height_r * height_r + width_r * width_r),
                            (byte)Math.Sqrt(height_g * height_g + width_g * width_g),
                            (byte)Math.Sqrt(height_b * height_b + width_b * width_b));
                        // �Q�l�����s��
                        if ((change.R + change.G + change.B) / 3 < 85)
                        {
                            change = Color.White;
                        }
                        else
                        {
                            change = Color.Black;
                        }
                        dst[yDst + x] = (uint)change.ToArgb();

                    }
                }
            }
            bitmapdst.UnlockBits(bitmapDataDst);
            bitmapsrc.UnlockBits(bitmapDataSrc);
            return bitmapdst;
        }
        enum Thin
        {
            UpperLeft = 2,
            LowerRight = 6,
            UpperRight = 0,
            LowerLeft = 4
        };
        //
        private Bitmap EdgeHilditch(Bitmap bitmapsrc)
        {
            return Thining(ExtractEdgeHilditch(bitmapsrc));
        }
        private Bitmap Thining(Bitmap bitmapnew)
        {
            Rectangle rectangle = new Rectangle(0, 0, bitmapnew.Width, bitmapnew.Height);
            BitmapData bitmapDataNew = bitmapnew.LockBits(rectangle, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            Bitmap bitmapold = new Bitmap(bitmapnew.Width, bitmapnew.Height);
            BitmapData bitmapDataOld = bitmapold.LockBits(rectangle, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            //
            unsafe
            {
                //uint* dst = (uint*)(void*)bitmapDataDst.Scan0;
                bool change;
                int time = 0;
                do{
                    change = false;
                    time++;
                    this.Text = "" + time;
                    //
                    Copy(bitmapDataNew, bitmapDataOld);//g, width, height); //�`��ƃR�s�[
                    //���ォ��א���
                    for (int j = 1; j < bitmapDataNew.Height - 1; j++)
                    {
                        for (int i = 1; i < bitmapDataNew.Width - 1; i++)
                        {
                            //QQQ:��������肭true�ɂȂ�Ȃ����玟��Thining���������Ȃ�
                            if (GetBitmap(bitmapDataOld, i, j) == Color.Black.ToArgb())
                            {
                                if (Thining(bitmapDataOld, bitmapDataNew, i, j, Thin.UpperLeft))
                                {
                                    change = true;
                                }
                            }
                            //if (old_pixels[i][j] == 1) thinImage(i, j, UPPER_LEFT);
                        }
                    }

                    Copy(bitmapDataNew, bitmapDataOld);//g, width, height); //�`��ƃR�s�[
                    //�E������א���
                    for (int j = bitmapDataNew.Height - 2; j >= 1; j--)
                    {
                        for (int i = bitmapDataNew.Width - 2; i >= 1; i--)
                        {
                            if (GetBitmap(bitmapDataOld, i, j) == Color.Black.ToArgb())
                            {
                                if (Thining(bitmapDataOld, bitmapDataNew, i, j, Thin.LowerRight))
                                {
                                    change = true;
                                }
                            }

                            //if (old_pixels[i][j] == 1) thinImage(i, j, LOWER_RIGHT);
                        }
                    }

                    Copy(bitmapDataNew, bitmapDataOld);//g, width, height); //�`��ƃR�s�[
                    //�E�ォ��א���
                    for (int j = 1; j < bitmapDataNew.Height - 1; j++)
                    {
                        for (int i = bitmapDataNew.Width - 2; i >= 1; i--)
                        {
                            if (GetBitmap(bitmapDataOld, i, j) == Color.Black.ToArgb())
                            {
                                if (Thining(bitmapDataOld, bitmapDataNew, i, j, Thin.UpperRight))
                                {
                                    change = true;
                                }
                            }

                            //if (old_pixels[i][j] == 1) thinImage(i, j, UPPER_RIGHT);
                        }
                    }

                    Copy(bitmapDataNew, bitmapDataOld);//g, width, height); //�`��ƃR�s�[
                    //��������א���
                    for (int j = bitmapDataNew.Height - 2; j >= 1; j--)
                    {
                        for (int i = 1; i < bitmapDataNew.Width - 1; i++)
                        {
                            if (GetBitmap(bitmapDataOld, i, j) == Color.Black.ToArgb())
                            {
                                if (Thining(bitmapDataOld, bitmapDataNew, i, j, Thin.LowerLeft))
                                {
                                    change = true;
                                }
                            }
                            //if (old_pixels[i][j] == 1) thinImage(i, j, LOWER_LEFT);
                        }
                    }

                }while(change);

            }
            bitmapold.UnlockBits(bitmapDataOld);
            bitmapnew.UnlockBits(bitmapDataNew);
            return bitmapnew;
        }
        private bool Thining(BitmapData bit_old, BitmapData bit_new, int x, int y, Thin thin)
        {
            int[] p = new int[8];
            int product, sum;
            int start = (int)thin;

            p[0] = (GetBitmap(bit_old, x - 1, y - 1) == Color.Black.ToArgb() ? 1 : 0);
            p[1] = (GetBitmap(bit_old, x - 1, y) == Color.Black.ToArgb() ? 1 : 0);// old_pixels[i - 1][j];
            p[2] = (GetBitmap(bit_old, x - 1, y + 1) == Color.Black.ToArgb() ? 1 : 0);// old_pixels[i - 1][j + 1];
            p[3] = (GetBitmap(bit_old, x, y + 1) == Color.Black.ToArgb() ? 1 : 0);// old_pixels[i][j + 1];
            p[4] = (GetBitmap(bit_old, x + 1, y + 1) == Color.Black.ToArgb() ? 1 : 0);// old_pixels[i + 1][j + 1];
            p[5] = (GetBitmap(bit_old, x + 1, y) == Color.Black.ToArgb() ? 1 : 0);// old_pixels[i + 1][j];
            p[6] = (GetBitmap(bit_old, x + 1, y - 1) == Color.Black.ToArgb() ? 1 : 0); //old_pixels[i + 1][j - 1];
            p[7] = (GetBitmap(bit_old, x, y - 1) == Color.Black.ToArgb() ? 1 : 0);// old_pixels[i][j - 1];

            unsafe
            {
                uint* st = (uint*)(void*)bit_new.Scan0;
                for (int k = start; k < start + 3; k++)
                {
                    product = p[k % 8] * p[(k + 1) % 8] * p[(k + 2) % 8];
                    sum = p[(k + 4) % 8] + p[(k + 5) % 8] + p[(k + 6) % 8];
                    if (product == 1 && sum == 0)
                    {
                        st[y * bit_new.Stride / sizeof(uint) + x] = (uint)Color.White.ToArgb();
                        //new_pixels[i][j] = 0;   //��������
                        return true;
                    }
                }
                return false;
            }
        }
        private void Copy(BitmapData from, BitmapData to)
        {
            unsafe
            {
                uint* st_from = (uint*)(void*)from.Scan0;
                uint* st_to = (uint*)(void*)to.Scan0;
                for (int y = 0; y < from.Height; y++)
                {
                    for (int x = 0; x < from.Width; x++)
                    {
                        st_to[y * to.Stride / sizeof(uint) + x] = st_from[y * from.Stride / sizeof(uint) + x];
                    }
                }
            }
        }
        private int GetBitmap(BitmapData data, int x, int y)
        {
            unsafe
            {
                uint* st = (uint*)(void*)data.Scan0;
                return (int)st[y * data.Stride / sizeof(uint) + x];
            }
        }
        /* ����GetColor == Color.Black�͐������Ȃ�����
        private Color GetColor(BitmapData data, int x, int y)
        {
            unsafe
            {
                return Color.FromArgb(GetBitmap(data, x, y));
            }
        }
         */

        private void �א���ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (�א���ToolStripMenuItem.Checked) { return; }
            ResetDrawOptionCheck();
            �א���ToolStripMenuItem.Checked = true;
            //
            DateTime start = DateTime.Now;
            this.Text = "������";
            pictureBox1.Image = Thining(ExtractObject(GrayScale(baseBitmap)));
            DateTime end = DateTime.Now;
            this.Text = "�����I���F" + (end - start);
        }
    }
}
/* �킩��Ȃ��Ă���Ȃ����Ƃɂ����R�[�h
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        struct BITMAPFILEHEADER
        {
            public ushort bfType;
            public uint bfSize;
            public ushort bfReserved1;
            public ushort bfReserved2;
            public uint bfOffBits;
        };
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        struct BITMAPINFOHEADER
        {
            public uint biSize;
            public int biWidth;
            public int biHeight;
            public ushort biPlanes;
            public ushort biBitCount;
            public uint biCompression;
            public uint biSizeImage;
            public int biXPelsPerMeter;
            public int biYPelsPerMeter;
            public uint biClrUsed;
            public uint biClrImportant;
            public const int BI_RGB = 0;
        };
        unsafe private static Bitmap BmpUnsafeNegative(string inputFileName)
        {
            FileStream inputFile = new FileStream(inputFileName, FileMode.Open);
            byte[] fileImage = new byte[inputFile.Length];
            inputFile.Read(fileImage, 0, (int)inputFile.Length);
            inputFile.Close();

            fixed (byte* basePtr = &fileImage[0])
            {
                BITMAPFILEHEADER* bitmapFileHeader = (BITMAPFILEHEADER*)basePtr;
                BITMAPINFOHEADER* bitmapInfoHeader = (BITMAPINFOHEADER*)(basePtr + sizeof(BITMAPFILEHEADER));
                byte* bits = basePtr + bitmapFileHeader->bfOffBits;
                if (bitmapInfoHeader->biBitCount != 24 || bitmapInfoHeader->biCompression != BITMAPINFOHEADER.BI_RGB)
                {
                    Console.WriteLine("Error: 24bit RGB Bitmap Only");
                    return BmpNegative(inputFileName);
                }
                int bytesInLine = (bitmapInfoHeader->biWidth + 3) / 4 * 4;
                int totalBytes = bytesInLine * bitmapInfoHeader->biHeight * 3;
                for (int i = 0; i < totalBytes; i++)
                {
                    *bits = (byte)~*bits;
                    bits++;
                }
            }
            FileStream outputFile = new FileStream("temp.tt",FileMode.Create);
            outputFile.Write(fileImage,0,fileImage.Length);
            outputFile.Close();
            Bitmap re_bit = new Bitmap("temp.tt");
            return re_bit;
        }

*/