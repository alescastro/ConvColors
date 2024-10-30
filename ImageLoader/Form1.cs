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
using static System.Resources.ResXFileRef;

namespace ImageLoader
{
    public partial class Form1 : Form
    {

        Bitmap img1;
        Bitmap img2;
        byte[,] vImg1Gray;

        byte[,] vImg1R;
        byte[,] vImg1G;
        byte[,] vImg1B;
        byte[,] vImg1A;


        Bitmap vimg1;
        Bitmap vimg2;
        byte[,] vImg2Gray;

        byte[,] vImg2R;
        byte[,] vImg2G;
        byte[,] vImg2B;
        byte[,] vImg2A;


        public Form1()
        {
            InitializeComponent();
        }

        private void btImg1_Click(object sender, EventArgs e)
        {
            // Configurações iniciais da OpenFileDialogBox
            var filePath = string.Empty;
            openFileDialog1.InitialDirectory = "C:\\Matlab";
            openFileDialog1.Filter = "TIFF image (*.tif)|*.tif|JPG image (*.jpg)|*.jpg|BMP image (*.bmp)|*.bmp|PNG image (*.png)|*.png|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            // Se um arquivo foi localizado com sucesso...
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Armnazena o path do arquivo de imagem
                filePath = openFileDialog1.FileName;


                bool bLoadImgOK = false;
                try
                {
                    img1 = new Bitmap(filePath);
                    img2 = new Bitmap(img1.Width, img1.Height);
                    bLoadImgOK = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erro ao abrir imagem...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    bLoadImgOK = false;
                }

                // Se a imagem carregou perfeitamente...
                if (bLoadImgOK == true)
                {
                    // Adiciona imagem na PictureBox
                    pictureBox1.Image = img1;
                    vImg1Gray = new byte[img1.Width, img1.Height];
                    vImg1R = new byte[img1.Width, img1.Height];
                    vImg1G = new byte[img1.Width, img1.Height];
                    vImg1B = new byte[img1.Width, img1.Height];
                    vImg1A = new byte[img1.Width, img1.Height];

                }

            }
        }
        private void btgImg2_Click(object sender, EventArgs e)
        {
            // Configurações iniciais da OpenFileDialogBox
            var filePath = string.Empty;
            openFileDialog1.InitialDirectory = "C:\\Matlab";
            openFileDialog1.Filter = "TIFF image (*.tif)|*.tif|JPG image (*.jpg)|*.jpg|BMP image (*.bmp)|*.bmp|PNG image (*.png)|*.png|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            // Se um arquivo foi localizado com sucesso...
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Armnazena o path do arquivo de imagem
                filePath = openFileDialog1.FileName;


                bool bLoadImgOK = false;
                try
                {
                    vimg1 = new Bitmap(filePath);
                    vimg2 = new Bitmap(vimg1.Width, vimg1.Height);
                    bLoadImgOK = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erro ao abrir imagem...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    bLoadImgOK = false;
                }

                // Se a imagem carregou perfeitamente...
                if (bLoadImgOK == true)
                {
                    // Adiciona imagem na PictureBox
                    pictureBox2.Image = vimg1;
                    vImg2Gray = new byte[vimg1.Width, vimg1.Height];
                    vImg2R = new byte[vimg1.Width, vimg1.Height];
                    vImg2G = new byte[vimg1.Width, vimg1.Height];
                    vImg2B = new byte[vimg1.Width, vimg1.Height];
                    vImg2A = new byte[vimg1.Width, vimg1.Height];

                }

            }
        }

        private void btnSomar_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null && pictureBox2.Image != null)
            {
                // Obtém as dimensões das imagens
                Bitmap img1 = new Bitmap(pictureBox1.Image);
                Bitmap img2 = new Bitmap(pictureBox2.Image);
                int width = Math.Min(img1.Width, img2.Width);
                int height = Math.Min(img1.Height, img2.Height);

                // Cria uma nova imagem para o resultado
                Bitmap resultado = new Bitmap(width, height);

                // Percorre cada pixel e soma as cores
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        Color cor1 = img1.GetPixel(x, y);
                        Color cor2 = img2.GetPixel(x, y);

                        // Soma os valores de cor e aplica o limite de 255
                        int r = Math.Min(cor1.R + cor2.R, 255);
                        int g = Math.Min(cor1.G + cor2.G, 255);
                        int b = Math.Min(cor1.B + cor2.B, 255);

                        // Define a cor resultante na nova imagem
                        resultado.SetPixel(x, y, Color.FromArgb(r, g, b));
                    }
                }

                // Mostra a imagem resultante na terceira PictureBox
                pictureBoxResult.Image = resultado;
            }
            else
            {
                MessageBox.Show("Por favor, carregue ambas as imagens.");
            }
        }

        private void btnSubt_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null && pictureBox2.Image != null)
            {
                // Obtém as dimensões das imagens
                Bitmap img1 = new Bitmap(pictureBox1.Image);
                Bitmap img2 = new Bitmap(pictureBox2.Image);
                int width = Math.Min(img1.Width, img2.Width);
                int height = Math.Min(img1.Height, img2.Height);

                // Cria uma nova imagem para o resultado
                Bitmap resultado = new Bitmap(width, height);

                // Percorre cada pixel e soma as cores
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        Color cor1 = img1.GetPixel(x, y);
                        Color cor2 = img2.GetPixel(x, y);

                        // Subtrai os valores de cor e aplica o limite para evitar underflow
                        int r = Math.Max(cor1.R - cor2.R, 0);
                        int g = Math.Max(cor1.G - cor2.G, 0);
                        int b = Math.Max(cor1.B - cor2.B, 0);

                        // Define a cor resultante na nova imagem
                        resultado.SetPixel(x, y, Color.FromArgb(r, g, b));
                    }
                }

                // Mostra a imagem resultante na terceira PictureBox
                pictureBoxResult.Image = resultado;
            }
            else
            {
                MessageBox.Show("Por favor, carregue ambas as imagens.");
            }
        }

        private void btnBrilho1_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {

                // Obtém as dimensões das imagem
                Bitmap img1 = new Bitmap(pictureBox1.Image);
                int width = (img1.Width);
                int height = (img1.Height);

                // Cria uma nova imagem para o resultado
                Bitmap resultado = new Bitmap(width, height);

                int txValor = Convert.ToInt32(txImg1.Text);
                if (txValor < 0)
                {
                    MessageBox.Show("Valor de contraste inválido. O valor deve ser maior que 0!", "Erro de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (txValor > 255)
                {
                    MessageBox.Show("Valor de contraste inválido. O valor deve ser menor que 255!", "Erro de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        Color cor1 = img1.GetPixel(x, y);

                        try
                        {
                            // Soma os valores de cor e aplica o limite de 255
                            int r = Math.Min(cor1.R + txValor, 255);
                            int g = Math.Min(cor1.G + txValor, 255);
                            int b = Math.Min(cor1.B + txValor, 255);

                            // Define a cor resultante na nova imagem
                            resultado.SetPixel(x, y, Color.FromArgb(r, g, b));
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("Por favor, insira um valor numérico válido.", "Erro de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        catch (OverflowException)
                        {
                            MessageBox.Show("O valor inserido é muito grande ou muito pequeno. Por favor, insira um valor entre 0 e 255.", "Erro de Overflow", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }

                pictureBoxResult.Image = resultado;
            }
            else
            {
                MessageBox.Show("Por favor, carregue uma imagem!", "Imagem não encontrada", MessageBoxButtons.OK);
            }
        }

        private void btnSubtBril1_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {

                // Obtém as dimensões das imagem
                Bitmap img1 = new Bitmap(pictureBox1.Image);
                int width = (img1.Width);
                int height = (img1.Height);

                // Cria uma nova imagem para o resultado
                Bitmap resultado = new Bitmap(width, height);

                int txValor = Convert.ToInt32(txImg1.Text);
                if (txValor < 0)
                {
                    MessageBox.Show("Valor de contraste inválido. O valor deve ser maior que 0!", "Erro de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (txValor > 255)
                {
                    MessageBox.Show("Valor de contraste inválido. O valor deve ser menor que 255!", "Erro de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        Color cor1 = img1.GetPixel(x, y);

                        try
                        {
                            // Soma os valores de cor e aplica o limite de 255
                            int r = Math.Max(cor1.R - txValor, 0);
                            int g = Math.Max(cor1.G - txValor, 0);
                            int b = Math.Max(cor1.B - txValor, 0);

                            // Define a cor resultante na nova imagem
                            resultado.SetPixel(x, y, Color.FromArgb(r, g, b));
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("Por favor, insira um valor numérico válido.", "Erro de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        catch (OverflowException)
                        {
                            MessageBox.Show("O valor inserido é muito grande ou muito pequeno. Por favor, insira um valor entre 0 e 255.", "Erro de Overflow", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }

                pictureBoxResult.Image = resultado;
            }
            else
            {
                MessageBox.Show("Por favor, carregue uma imagem!", "Imagem não encontrada", MessageBoxButtons.OK);
            }
        }

        private void btnBrilho2_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image != null)
            {

                // Obtém as dimensões das imagem
                Bitmap img1 = new Bitmap(pictureBox2.Image);
                int width = (img1.Width);
                int height = (img1.Height);

                // Cria uma nova imagem para o resultado
                Bitmap resultado = new Bitmap(width, height);

                int txValor = Convert.ToInt32(txImg2.Text);
                if (txValor < 0)
                {
                    MessageBox.Show("Valor de contraste inválido. O valor deve ser maior que 0!", "Erro de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (txValor > 255)
                {
                    MessageBox.Show("Valor de contraste inválido. O valor deve ser menor que 255!", "Erro de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        Color cor1 = img1.GetPixel(x, y);

                        try
                        {
                            // Soma os valores de cor e aplica o limite de 255
                            int r = Math.Min(cor1.R + txValor, 255);
                            int g = Math.Min(cor1.G + txValor, 255);
                            int b = Math.Min(cor1.B + txValor, 255);

                            // Define a cor resultante na nova imagem
                            resultado.SetPixel(x, y, Color.FromArgb(r, g, b));
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("Por favor, insira um valor numérico válido.", "Erro de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        catch (OverflowException)
                        {
                            MessageBox.Show("O valor inserido é muito grande ou muito pequeno. Por favor, insira um valor entre 0 e 255.", "Erro de Overflow", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }

                pictureBoxResult.Image = resultado;
            }
            else
            {
                MessageBox.Show("Por favor, carregue uma imagem!", "Imagem não encontrada", MessageBoxButtons.OK);
            }
        }


        private void btnSubtBril2_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image != null)
            {

                // Obtém as dimensões das imagem
                Bitmap img1 = new Bitmap(pictureBox2.Image);
                int width = (img1.Width);
                int height = (img1.Height);

                // Cria uma nova imagem para o resultado
                Bitmap resultado = new Bitmap(width, height);

                int txValor = Convert.ToInt32(txImg2.Text);
                if (txValor < 0)
                {
                    MessageBox.Show("Valor de contraste inválido. O valor deve ser maior que 0!", "Erro de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (txValor > 255)
                {
                    MessageBox.Show("Valor de contraste inválido. O valor deve ser menor que 255!", "Erro de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        Color cor1 = img1.GetPixel(x, y);

                        try
                        {
                            // Soma os valores de cor e aplica o limite de 255
                            int r = Math.Max(cor1.R - txValor, 0);
                            int g = Math.Max(cor1.G - txValor, 0);
                            int b = Math.Max(cor1.B - txValor, 0);

                            // Define a cor resultante na nova imagem
                            resultado.SetPixel(x, y, Color.FromArgb(r, g, b));
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("Por favor, insira um valor numérico válido.", "Erro de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        catch (OverflowException)
                        {
                            MessageBox.Show("O valor inserido é muito grande ou muito pequeno. Por favor, insira um valor entre 0 e 255.", "Erro de Overflow", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }

                pictureBoxResult.Image = resultado;
            }
            else
            {
                MessageBox.Show("Por favor, carregue uma imagem!", "Imagem não encontrada", MessageBoxButtons.OK);
            }
        }

        private void btnSumCont1_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {

                // Obtém as dimensões das imagem
                Bitmap img1 = new Bitmap(pictureBox1.Image);
                int width = (img1.Width);
                int height = (img1.Height);

                // Cria uma nova imagem para o resultado
                Bitmap resultado = new Bitmap(width, height);

                // Cria matrizes para armazenar os valores de R, G e B como floats
                float[,] pixelIntensityR = new float[width, height];
                float[,] pixelIntensityG = new float[width, height];
                float[,] pixelIntensityB = new float[width, height];

                float.TryParse(txImg1.Text, out float txValor);

                if(txValor < 0)
                {
                    MessageBox.Show("Valor de contraste inválido. O valor deve ser maior que 0!", "Erro de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if(txValor > 2)
                {
                    MessageBox.Show("Valor de contraste inválido. O valor deve estar entre 0 e 1!", "Erro de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        Color cor1 = img1.GetPixel(x, y);

                        float r = Math.Min(255, cor1.R * txValor);
                        float g = Math.Min(255, cor1.G * txValor);
                        float b = Math.Min(255, cor1.B * txValor);

                        pixelIntensityR[x, y] = r;
                        pixelIntensityG[x, y] = g;
                        pixelIntensityB[x, y] = b;

                        Color novaCor = Color.FromArgb((byte)r, (byte)g, (byte)b);
                        resultado.SetPixel(x, y, novaCor);
                    }
                }

                // Mostra a imagem resultante na terceira PictureBox
                pictureBoxResult.Image = resultado;
            }
            else
            {
                MessageBox.Show("Por favor, carregue uma imagem!", "Imagem não encontrada", MessageBoxButtons.OK);
            }
        }

        private void btnSumCont2_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image != null)
            {

                // Obtém as dimensões das imagem
                Bitmap img1 = new Bitmap(pictureBox2.Image);
                int width = (img1.Width);
                int height = (img1.Height);

                // Cria uma nova imagem para o resultado
                Bitmap resultado = new Bitmap(width, height);

                // Cria matrizes para armazenar os valores de R, G e B como floats
                float[,] pixelIntensityR = new float[width, height];
                float[,] pixelIntensityG = new float[width, height];
                float[,] pixelIntensityB = new float[width, height];

                float.TryParse(txImg2.Text, out float txValor);
                if (txValor < 0)
                {
                    MessageBox.Show("Valor de contraste inválido. O valor deve ser maior que 0", "Erro de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (txValor > 2)
                {
                    MessageBox.Show("Valor de contraste inválido. O valor deve estar entre 0 e 1!", "Erro de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        Color cor1 = img1.GetPixel(x, y);

                        float r = Math.Max(0, Math.Min(255, cor1.R * txValor));
                        float g = Math.Max(0, Math.Min(255, cor1.G * txValor));
                        float b = Math.Max(0, Math.Min(255, cor1.B * txValor));

                        pixelIntensityR[x, y] = r;
                        pixelIntensityG[x, y] = g;
                        pixelIntensityB[x, y] = b;

                        Color novaCor = Color.FromArgb((byte)r, (byte)g, (byte)b);
                        resultado.SetPixel(x, y, novaCor);
                    }
                }

                // Mostra a imagem resultante na terceira PictureBox
                pictureBoxResult.Image = resultado;
            }
            else
            {
                MessageBox.Show("Por favor, carregue uma imagem!", "Imagem não encontrada", MessageBoxButtons.OK);
            }
        }

        private void btnSubtCont1_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {

                // Obtém as dimensões das imagem
                Bitmap img1 = new Bitmap(pictureBox1.Image);
                int width = (img1.Width);
                int height = (img1.Height);

                // Cria uma nova imagem para o resultado
                Bitmap resultado = new Bitmap(width, height);

                // Cria matrizes para armazenar os valores de R, G e B como floats
                float[,] pixelIntensityR = new float[width, height];
                float[,] pixelIntensityG = new float[width, height];
                float[,] pixelIntensityB = new float[width, height];

                float.TryParse(txImg1.Text, out float txValor);

                if (txValor < 0)
                {
                    MessageBox.Show("Valor de contraste inválido. O valor deve ser maior que 0");
                    return;
                }

                if (txValor > 2)
                {
                    MessageBox.Show("Valor de contraste inválido. O valor deve estar entre 0 e 1!", "Erro de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        Color cor1 = img1.GetPixel(x, y);

                        float r = Math.Min(255, cor1.R / txValor);
                        float g = Math.Min(255, cor1.G / txValor);
                        float b = Math.Min(255, cor1.B / txValor);

                        pixelIntensityR[x, y] = r;
                        pixelIntensityG[x, y] = g;
                        pixelIntensityB[x, y] = b;

                        Color novaCor = Color.FromArgb((byte)r, (byte)g, (byte)b);
                        resultado.SetPixel(x, y, novaCor);
                    }
                }

                // Mostra a imagem resultante na terceira PictureBox
                pictureBoxResult.Image = resultado;
            }
            else
            {
                MessageBox.Show("Por favor, carregue uma imagem!", "Imagem não encontrada", MessageBoxButtons.OK);
            }
        }

        private void btnSubtCont2_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image != null)
            {

                // Obtém as dimensões das imagem
                Bitmap img1 = new Bitmap(pictureBox2.Image);
                int width = (img1.Width);
                int height = (img1.Height);

                // Cria uma nova imagem para o resultado
                Bitmap resultado = new Bitmap(width, height);

                // Cria matrizes para armazenar os valores de R, G e B como floats
                float[,] pixelIntensityR = new float[width, height];
                float[,] pixelIntensityG = new float[width, height];
                float[,] pixelIntensityB = new float[width, height];

                float.TryParse(txImg2.Text, out float txValor);
                if (txValor < 0)
                {
                    MessageBox.Show("Valor de contraste inválido. O valor deve ser maior que 0");
                    return;
                }

                if (txValor > 2)
                {
                    MessageBox.Show("Valor de contraste inválido. O valor deve estar entre 0 e 1!", "Erro de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        Color cor1 = img1.GetPixel(x, y);

                        float r = Math.Max(0, cor1.R / txValor);
                        float g = Math.Max(0, cor1.G / txValor);
                        float b = Math.Max(0, cor1.B / txValor);

                        pixelIntensityR[x, y] = r;
                        pixelIntensityG[x, y] = g;
                        pixelIntensityB[x, y] = b;

                        Color novaCor = Color.FromArgb((byte)r, (byte)g, (byte)b);
                        resultado.SetPixel(x, y, novaCor);
                    }
                }

                // Mostra a imagem resultante na terceira PictureBox
                pictureBoxResult.Image = resultado;
            }
            else
            {
                MessageBox.Show("Por favor, carregue uma imagem!", "Imagem não encontrada", MessageBoxButtons.OK);
            }
        }

        private void btnCz1_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                // Percorre todos os pixels da imagem...
                for (int i = 0; i < img1.Width; i++)
                {
                    for (int j = 0; j < img1.Height; j++)
                    {
                        Color pixel = img1.GetPixel(i, j);

                        // Para imagens em escala de cinza, extrair o valor do pixel com...
                        //byte pixelIntensity = Convert.ToByte((pixel.R + pixel.G + pixel.B) / 3);
                        byte pixelIntensity = Convert.ToByte((pixel.R + pixel.G + pixel.B) / 3);
                        vImg1Gray[i, j] = pixelIntensity;

                        // Para imagens RGB, extrair o valor do pixel com...
                        byte R = pixel.R;
                        byte G = pixel.G;
                        byte B = pixel.B;
                        byte A = pixel.A;

                        vImg1R[i, j] = R;
                        vImg1G[i, j] = G;
                        vImg1B[i, j] = B;
                        vImg1A[i, j] = A;

                        Color cor = Color.FromArgb(
                            255,
                            vImg1Gray[i, j],
                            vImg1Gray[i, j],
                            vImg1Gray[i, j]);

                        img2.SetPixel(i, j, cor);
                    }
                }

                pictureBoxResult.Image = img2;
            }
            else
            {
                MessageBox.Show("Por favor, carregue uma imagem!", "Imagem não encontrada", MessageBoxButtons.OK);
            }
        }

        private void btnCz2_Click(object sender, EventArgs e)
        {

            if (pictureBox2.Image != null)
            {
                // Percorre todos os pixels da imagem...
                for (int i = 0; i < vimg1.Width; i++)
                {
                    for (int j = 0; j < vimg1.Height; j++)
                    {
                        Color pixel = vimg1.GetPixel(i, j);

                        // Para imagens em escala de cinza, extrair o valor do pixel com...
                        //byte pixelIntensity = Convert.ToByte((pixel.R + pixel.G + pixel.B) / 3);
                        byte pixelIntensity = Convert.ToByte((pixel.R + pixel.G + pixel.B) / 3);
                        vImg2Gray[i, j] = pixelIntensity;

                        // Para imagens RGB, extrair o valor do pixel com...
                        byte R = pixel.R;
                        byte G = pixel.G;
                        byte B = pixel.B;
                        byte A = pixel.A;

                        vImg2R[i, j] = R;
                        vImg2G[i, j] = G;
                        vImg2B[i, j] = B;
                        vImg2A[i, j] = A;

                        Color cor = Color.FromArgb(
                            255,
                            vImg2Gray[i, j],
                            vImg2Gray[i, j],
                            vImg2Gray[i, j]);

                        vimg2.SetPixel(i, j, cor);
                    }
                }

                pictureBoxResult.Image = vimg2;
            }
            else
            {
                MessageBox.Show("Por favor, carregue uma imagem!", "Imagem não encontrada", MessageBoxButtons.OK);
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (pictureBoxResult.Image != null)
            {
                // Configura o diálogo de salvar arquivo
                saveFileDialog1.InitialDirectory = "C:\\Matlab"; // Diretório inicial
                saveFileDialog1.Filter = "JPG image (*.jpg)|*.jpg|TIFF image (*.tif)|*.tif|BMP image (*.bmp)|*.bmp|PNG image (*.png)|*.png|All files (*.*)|*.*";
                saveFileDialog1.DefaultExt = "jpg"; // Extensão padrão como JPG
                saveFileDialog1.FileName = ""; // Nome de arquivo padrão
                saveFileDialog1.OverwritePrompt = true; // Pergunta antes de sobrescrever um arquivo
                saveFileDialog1.Title = "Salvar Imagem"; // Título do diálogo
                saveFileDialog1.AddExtension = true; // Adiciona extensão ao arquivo

                // Exibe o diálogo e verifica se o usuário clicou em "Salvar"
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    // Obtém o caminho do arquivo selecionado
                    string filePath = saveFileDialog1.FileName;

                    try
                    {
                        // Salva a imagem no caminho selecionado
                        pictureBoxResult.Image.Save(filePath);
                        MessageBox.Show("Imagem salva com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        // Exibe uma mensagem de erro se a operação falhar
                        MessageBox.Show($"Erro ao salvar a imagem: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Não há resultado a ser salvo.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnFlip1_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {

                // Obtém as dimensões das imagem
                Bitmap img1 = new Bitmap(pictureBox1.Image);
                int width = (img1.Width);
                int height = (img1.Height);

                // Cria uma nova imagem para o resultado
                Bitmap resultado = new Bitmap(width, height);

                // Percorre todos os pixels da imagem...
                for (int i = 0; i < img1.Width; i++)
                {
                    for (int j = 0; j < img1.Height; j++)
                    {

                     Color pixel = img1.GetPixel(i, j);


                     resultado.SetPixel(width - 1 - i, j, pixel);
                    }
                }
                pictureBoxResult.Image = resultado;
            }
            else
            {
                MessageBox.Show("Por favor, carregue uma imagem!", "Imagem não encontrada", MessageBoxButtons.OK);
            }
        }

        private void btnFlip2_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image != null)
            {

                // Obtém as dimensões das imagem
                Bitmap img1 = new Bitmap(pictureBox2.Image);
                int width = (img1.Width);
                int height = (img1.Height);

                // Cria uma nova imagem para o resultado
                Bitmap resultado = new Bitmap(width, height);

                // Percorre todos os pixels da imagem...
                for (int i = 0; i < img1.Width; i++)
                {
                    for (int j = 0; j < img1.Height; j++)
                    {

                        Color pixel = img1.GetPixel(i, j);


                        resultado.SetPixel(width - 1 - i, j, pixel);
                    }
                }
                pictureBoxResult.Image = resultado;
            }
            else
            {
                MessageBox.Show("Por favor, carregue uma imagem!", "Imagem não encontrada", MessageBoxButtons.OK);
            }
        }

        private void BtnGirar1_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {

                // Obtém as dimensões das imagem
                Bitmap img1 = new Bitmap(pictureBox1.Image);
                int width = (img1.Width);
                int height = (img1.Height);

                // Cria uma nova imagem para o resultado
                Bitmap resultado = new Bitmap(width, height);

                // Percorre todos os pixels da imagem...
                for (int i = 0; i < img1.Width; i++)
                {
                    for (int j = 0; j < img1.Height; j++)
                    {

                        Color pixel = img1.GetPixel(i, j);


                        resultado.SetPixel(i, height - 1 - j, pixel);
                    }
                }
                pictureBoxResult.Image = resultado;
            }
            else
            {
                MessageBox.Show("Por favor, carregue uma imagem!", "Imagem não encontrada", MessageBoxButtons.OK);
            }
        }

        private void BtnGirar2_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image != null)
            {

                // Obtém as dimensões das imagem
                Bitmap img1 = new Bitmap(pictureBox2.Image);
                int width = (img1.Width);
                int height = (img1.Height);

                // Cria uma nova imagem para o resultado
                Bitmap resultado = new Bitmap(width, height);

                // Percorre todos os pixels da imagem...
                for (int i = 0; i < img1.Width; i++)
                {
                    for (int j = 0; j < img1.Height; j++)
                    {

                        Color pixel = img1.GetPixel(i, j);


                        resultado.SetPixel(i, height - 1 - j, pixel);
                    }
                }
                pictureBoxResult.Image = resultado;
            }
            else
            {
                MessageBox.Show("Por favor, carregue uma imagem!", "Imagem não encontrada", MessageBoxButtons.OK);
            }
        }

        private void btnDiferenca_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null && pictureBox2.Image != null)
            {
                // Obtém as dimensões das imagens
                Bitmap img1 = new Bitmap(pictureBox1.Image);
                Bitmap img2 = new Bitmap(pictureBox2.Image);

                int width1 = img1.Width;
                int height1 = img1.Height;

                int width2 = img2.Width;
                int height2 = img2.Height;

                if (width1 != width2 || height1 != height2)
                {
                    MessageBox.Show("As imagens devem ter o mesmo tamanho!", "Erro", MessageBoxButtons.OK);
                    return;
                }

                Bitmap resultado = new Bitmap(width1, height1);

                // Percorre todos os pixels da imagem
                for (int i = 0; i < width1; i++)
                {
                    for (int j = 0; j < height1; j++)
                    {
                        Color pixel1 = img1.GetPixel(i, j);
                        Color pixel2 = img2.GetPixel(i, j);

                        int r = Math.Abs(pixel1.R - pixel2.R);
                        int g = Math.Abs(pixel1.G - pixel2.G);
                        int b = Math.Abs(pixel1.B - pixel2.B);

                        Color resultadoPixel = Color.FromArgb(r, g, b);

                        resultado.SetPixel(i, j, resultadoPixel);
                    }
                }

                // Exibe o resultado na PictureBox
                pictureBoxResult.Image = resultado;
            }
            else
            {
                MessageBox.Show("Por favor, carregue ambas as imagens!", "Imagem não encontrada", MessageBoxButtons.OK);
            }
        }

        private void btnBlend_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null && pictureBox2.Image != null)
            {
                // Obtém as dimensões das imagens
                Bitmap img1 = new Bitmap(pictureBox1.Image);
                Bitmap img2 = new Bitmap(pictureBox2.Image);

                int width1 = img1.Width;
                int height1 = img1.Height;

                int width2 = img2.Width;
                int height2 = img2.Height;

                if (width1 != width2 || height1 != height2)
                {
                    MessageBox.Show("As imagens devem ter o mesmo tamanho!", "Erro", MessageBoxButtons.OK);
                    return;
                }

                if (!float.TryParse(txBlend.Text, out float txValor) || txValor < 0 || txValor > 1)
                {
                    MessageBox.Show("Valor de blending inválido! Deve estar entre 0 e 1.", "Erro", MessageBoxButtons.OK);
                    return;
                }

                // Cria matrizes para armazenar os valores de R, G e B como floats
                float[,] pixelIntensityR = new float[width1, height1];
                float[,] pixelIntensityG = new float[width1, height1];
                float[,] pixelIntensityB = new float[width1, height1];

                Bitmap resultado = new Bitmap(width1, height1);

                // Percorre todos os pixels da imagem
                for (int i = 0; i < width1; i++)
                {
                    for (int j = 0; j < height1; j++)
                    {
                        Color pixel1 = img1.GetPixel(i, j);
                        Color pixel2 = img2.GetPixel(i, j);

                        int r = (int)Math.Min(Math.Max(txValor * pixel1.R + (1 - txValor) * pixel2.R, 0), 255);
                        int g = (int)Math.Min(Math.Max(txValor * pixel1.G + (1 - txValor) * pixel2.G, 0), 255);
                        int b = (int)Math.Min(Math.Max(txValor * pixel1.B + (1 - txValor) * pixel2.B, 0),255);

                        Color resultadoPixel = Color.FromArgb(r, g, b);

                        resultado.SetPixel(i, j, resultadoPixel);
                    }
                }

                // Exibe o resultado na PictureBox
                pictureBoxResult.Image = resultado;
            }
            else
            {
                MessageBox.Show("Por favor, carregue ambas as imagens!", "Imagem não encontrada", MessageBoxButtons.OK);
            }
        }

        private void btnMed_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null && pictureBox2.Image != null)
            {
                // Obtém as dimensões das imagens
                Bitmap img1 = new Bitmap(pictureBox1.Image);
                Bitmap img2 = new Bitmap(pictureBox2.Image);

                int width1 = img1.Width;
                int height1 = img1.Height;

                int width2 = img2.Width;
                int height2 = img2.Height;

                if (width1 != width2 || height1 != height2)
                {
                    MessageBox.Show("As imagens devem ter o mesmo tamanho!", "Erro", MessageBoxButtons.OK);
                    return;
                }

       

                Bitmap resultado = new Bitmap(width1, height1);

                // Percorre todos os pixels da imagem
                for (int i = 0; i < width1; i++)
                {
                    for (int j = 0; j < height1; j++)
                    {
                        Color pixel1 = img1.GetPixel(i, j);
                        Color pixel2 = img2.GetPixel(i, j);

                        int r = ((pixel1.R + pixel2.R) / 2);
                        int g = ((pixel1.G + pixel2.G) / 2);
                        int b = ((pixel1.B + pixel2.B) / 2);

                        Color resultadoPixel = Color.FromArgb(r, g, b);

                        resultado.SetPixel(i, j, resultadoPixel);
                    }
                }

                // Exibe o resultado na PictureBox
                pictureBoxResult.Image = resultado;
            }
            else
            {
                MessageBox.Show("Por favor, carregue ambas as imagens!", "Imagem não encontrada", MessageBoxButtons.OK);
            }
        }

        private void btn_Not_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                Bitmap img = new Bitmap(pictureBox1.Image);

                int width = img.Width;
                int height = img.Height;

                Bitmap resultado = new Bitmap(width, height);

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        Color pixel = img.GetPixel(x, y);

                        int r = pixel.R;

                        int rInvertido = (r == 0) ? 255 : 0; // condição ternária - testa a condição de r == 0, se true inverte para 255, se false, transforma o pixel para 0.

                        Color pixelInvertido = Color.FromArgb(rInvertido, rInvertido, rInvertido);

                        resultado.SetPixel(x, y, pixelInvertido);
                    }
                }

                pictureBoxResult.Image = resultado;
            }
            else
            {
                MessageBox.Show("Por favor, carregue uma imagem!", "Imagem não encontrada", MessageBoxButtons.OK);
            }
        }

        private void btn_Or_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null && pictureBox2.Image != null)
            {
                Bitmap img1 = new Bitmap(pictureBox1.Image);
                Bitmap img2 = new Bitmap(pictureBox2.Image);

                int width1 = img1.Width;
                int height1 = img1.Height;

                int width2 = img2.Width;
                int height2 = img2.Height;

                if (width1 != width2 || height1 != height2)
                {
                    MessageBox.Show("As imagens devem ter o mesmo tamanho!", "Erro", MessageBoxButtons.OK);
                    return;
                }

                Bitmap imgOr = new Bitmap(img1.Width, img1.Height);

                for (int i = 0; i < img1.Width; i++)
                {
                    for (int j = 0; j < img1.Height; j++)
                    {
                        Color pixelP = img1.GetPixel(i, j);
                        Color pixelQ = img2.GetPixel(i, j);

                        byte r = (byte)((pixelP.R | pixelQ.R));
                        byte g = (byte)((pixelP.G | pixelQ.G));
                        byte b = (byte)((pixelP.B | pixelQ.B));

                        imgOr.SetPixel(i, j, Color.FromArgb(r, g, b));
                    }
                }

                pictureBoxResult.Image = imgOr;
            }
            else
            {
                MessageBox.Show("Por favor, carregue uma imagem!", "Imagem não encontrada", MessageBoxButtons.OK);
            }
        }

        private void btn_Xor_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null && pictureBox2.Image != null)
            {
                // Obtém as dimensões das imagens
                Bitmap img1 = new Bitmap(pictureBox1.Image);
                Bitmap img2 = new Bitmap(pictureBox2.Image);

                int width1 = img1.Width;
                int height1 = img1.Height;

                int width2 = img2.Width;
                int height2 = img2.Height;

                if (width1 != width2 || height1 != height2)
                {
                    MessageBox.Show("As imagens devem ter o mesmo tamanho!", "Erro", MessageBoxButtons.OK);
                    return;
                }

                Bitmap imgXor = new Bitmap(img1.Width, img1.Height);

                for (int i = 0; i < img1.Width; i++)
                {
                    for (int j = 0; j < img1.Height; j++)
                    {
                        Color pixelP = img1.GetPixel(i, j);
                        Color pixelQ = img2.GetPixel(i, j);

                        // Aplica a operação XOR lógica
                        byte r = (byte)(pixelP.R ^ pixelQ.R);
                        byte g = (byte)(pixelP.G ^ pixelQ.G);
                        byte b = (byte)(pixelP.B ^ pixelQ.B);

                        imgXor.SetPixel(i, j, Color.FromArgb(r, g, b));
                    }
                }

                pictureBoxResult.Image = imgXor;
            }
            else
            {
                MessageBox.Show("Por favor, carregue ambas as imagens!", "Imagem não encontrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btn_And_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null && pictureBox2.Image != null)
            {
                // Obtém as dimensões das imagens
                Bitmap img1 = new Bitmap(pictureBox1.Image);
                Bitmap img2 = new Bitmap(pictureBox2.Image);

                int width1 = img1.Width;
                int height1 = img1.Height;

                int width2 = img2.Width;
                int height2 = img2.Height;

                if (width1 != width2 || height1 != height2)
                {
                    MessageBox.Show("As imagens devem ter o mesmo tamanho!", "Erro", MessageBoxButtons.OK);
                    return;
                }

                Bitmap imgAnd = new Bitmap(img1.Width, img1.Height);

                // Calcula o AND lógico
                for (int i = 0; i < img1.Width; i++)
                {
                    for (int j = 0; j < img1.Height; j++)
                    {
                        // Obtém os pixels das duas imagens
                        Color pixelP = img1.GetPixel(i, j);
                        Color pixelQ = img2.GetPixel(i, j);

                        byte r = (byte)((pixelP.R & pixelQ.R));
                        byte g = (byte)((pixelP.G & pixelQ.G));
                        byte b = (byte)((pixelP.B & pixelQ.B));

                        imgAnd.SetPixel(i, j, Color.FromArgb(r, g, b));
                    }
                }

                pictureBoxResult.Image = imgAnd;
            }
            else
            {
                MessageBox.Show("Por favor, carregue ambas as imagens!", "Imagem não encontrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnEqImg1_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                Bitmap img1 = new Bitmap(pictureBox1.Image);
                byte[,] hvImg = new byte[img1.Width, img1.Height];
                byte[,] hvNewImg = new byte[img1.Width, img1.Height];
                int width = img1.Width;
                int height = img1.Height;


                int[] histogramInit = new int[256];
                int[] histogramFinal = new int[256];

                // zerar os arrays
                for (int i = 0; i < histogramInit.Length; i++)
                {
                    histogramInit[i] = 0;
                }

                for (int i = 0; i < histogramFinal.Length; i++)
                {
                    histogramFinal[i] = 0;
                }


                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        Color pixelColor = img1.GetPixel(i, j);

                        int pixelValue = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;

                        hvImg[i, j] = Convert.ToByte(pixelValue);

                        // incrementa o valor correspondente no histograma
                        histogramInit[pixelValue]++;
                    }
                }

                // array do CFD
                int[] CFD = new int[256];
                CFD[0] = histogramInit[0];

                // calcula do CFD
                for (int i = 1; i < 256; i++)
                {
                    if (i == 0)
                    {
                        CFD[i] = histogramInit[i];
                    }
                    else
                    {
                        CFD[i] = histogramInit[i] + CFD[i - 1];
                    }
                }

                double dCFDmin = Convert.ToDouble(CFD.First(element => (element > 0)));

                // Atualizar CHART Imagem Original
                for (int i = 0; i < 256; i++)
                {
                    chart1.Series["Series1"].Points.AddXY(i, histogramInit[i]);
                }

                // calcular a nova cor de cada pixel

                Bitmap img2 = new Bitmap(pictureBox1.Image);

                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {

                        int pixelNew = Convert.ToInt32(
                            Math.Floor(
                                (((Convert.ToDouble(CFD[hvImg[i, j]] - dCFDmin) /
                                (Convert.ToDouble(img1.Width * img1.Height) - dCFDmin)) * 255.0)
                            )));

                        hvNewImg[i, j] = Convert.ToByte(pixelNew);

                        Color cor = Color.FromArgb(
                            255,
                            hvNewImg[i, j],
                            hvNewImg[i, j],
                            hvNewImg[i, j]
                            );

                        img2.SetPixel(i, j, cor);
                    }
                } pictureBoxResult.Image = img2;

                // CALCULAR HISTOGRAMA IMAGEM NOVA

                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {

                        // incrementa o valor correspondente no histograma
                        histogramFinal[hvNewImg[i, j]] ++;
                    }
                }

                // Atualizar CHART Imagem Final
                for (int i = 0; i < 256; i++)
                {
                    chart2.Series["Series1"].Points.AddXY(i, histogramFinal[i]);
                }

            }
            else
            {
                MessageBox.Show("Por favor, carregue uma imagem!", "Imagem não encontrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnImgEq2_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image != null)
            {
                Bitmap img1 = new Bitmap(pictureBox2.Image);
                byte[,] hvImg = new byte[img1.Width, img1.Height];
                byte[,] hvNewImg = new byte[img1.Width, img1.Height];
                int width = img1.Width;
                int height = img1.Height;


                int[] histogramInit = new int[256];
                int[] histogramFinal = new int[256];

                // zerar os arrays
                for (int i = 0; i < histogramInit.Length; i++)
                {
                    histogramInit[i] = 0;
                }

                for (int i = 0; i < histogramFinal.Length; i++)
                {
                    histogramFinal[i] = 0;
                }


                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        Color pixelColor = img1.GetPixel(i, j);

                        int pixelValue = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;

                        hvImg[i, j] = Convert.ToByte(pixelValue);

                        // incrementa o valor correspondente no histograma
                        histogramInit[pixelValue]++;
                    }
                }

                // array do CFD
                int[] CFD = new int[256];
                CFD[0] = histogramInit[0];

                // calcula do CFD
                for (int i = 1; i < 256; i++)
                {
                    if (i == 0)
                    {
                        CFD[i] = histogramInit[i];
                    }
                    else
                    {
                        CFD[i] = histogramInit[i] + CFD[i - 1];
                    }
                }

                double dCFDmin = Convert.ToDouble(CFD.First(element => (element > 0)));

                // Atualizar CHART Imagem Original
                for (int i = 0; i < 256; i++)
                {
                    chart4.Series["Series1"].Points.AddXY(i, histogramInit[i]);
                }

                // calcular a nova cor de cada pixel

                Bitmap img2 = new Bitmap(pictureBox2.Image);

                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {

                        int pixelNew = Convert.ToInt32(
                            Math.Floor(
                                (((Convert.ToDouble(CFD[hvImg[i, j]] - dCFDmin) /
                                (Convert.ToDouble(img1.Width * img1.Height) - dCFDmin)) * 255.0)
                            )));

                        hvNewImg[i, j] = Convert.ToByte(pixelNew);

                        Color cor = Color.FromArgb(
                            255,
                            hvNewImg[i, j],
                            hvNewImg[i, j],
                            hvNewImg[i, j]
                            );

                        img2.SetPixel(i, j, cor);
                    }
                }
                pictureBoxResult.Image = img2;

                // CALCULAR HISTOGRAMA IMAGEM NOVA

                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {

                        // incrementa o valor correspondente no histograma
                        histogramFinal[hvNewImg[i, j]]++;
                    }
                }

                // Atualizar CHART Imagem Final
                for (int i = 0; i < 256; i++)
                {
                    chart3.Series["Series1"].Points.AddXY(i, histogramFinal[i]);
                }
            }
            else
            {
                MessageBox.Show("Por favor, carregue uma imagem!", "Imagem não encontrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnLimpar_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            pictureBox2.Image = null;
            pictureBoxResult.Image = null;
            chart1.Series.Clear();
            chart2.Series.Clear();
            chart3.Series.Clear();
            chart4.Series.Clear();
        }

        private void btnLimiar_Click(object sender, EventArgs e)
        {
            // verificação de radio button
            if (!rbPictureBox1.Checked && !rbPictureBox2.Checked)
            {
                MessageBox.Show("Por favor, selecione uma PictureBox para aplicar o limiar.", "Nenhuma PictureBox Selecionada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Bitmap img1 = null;
            PictureBox targetPictureBox = null;

            // se rbPictureBox1 for marcado
            if (rbPictureBox1.Checked)
            {
                if (pictureBox1.Image != null)
                {
                    img1 = new Bitmap(pictureBox1.Image);
                    targetPictureBox = pictureBox1;
                }
                else
                {
                    MessageBox.Show("Por favor, carregue uma imagem na PictureBox 1!", "Imagem não encontrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else if (rbPictureBox2.Checked)
            {
                if (pictureBox2.Image != null)
                {
                    img1 = new Bitmap(pictureBox2.Image);
                    targetPictureBox = pictureBox2;
                }
                else
                {
                    MessageBox.Show("Por favor, carregue uma imagem na PictureBox 2!", "Imagem não encontrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // verificação do intervalo do limiar
            if (int.TryParse(txLimiar.Text, out int limiarValor))
            {
                if (limiarValor < 0 || limiarValor > 255)
                {
                    MessageBox.Show("O valor do limiar deve estar entre 0 e 255.", "Valor fora do intervalo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Bitmap img2 = new Bitmap(img1.Width, img1.Height);
                bool isGrayScale = IsGrayScale(img1);

                for (int i = 0; i < img1.Width; i++)
                {
                    for (int j = 0; j < img1.Height; j++)
                    {
                        Color pixel = img1.GetPixel(i, j);

                        int pixelIntensity;
                        if (isGrayScale)
                        {
                            pixelIntensity = pixel.R;
                        }
                        else
                        {
                            pixelIntensity = (pixel.R + pixel.G + pixel.B) / 3;
                        }

                        int valorBinario = pixelIntensity >= limiarValor ? 255 : 0;
                        Color cor = Color.FromArgb(255, valorBinario, valorBinario, valorBinario);
                        img2.SetPixel(i, j, cor);
                    }
                }

                // atualiza resultado conforme check do radio button
                if (targetPictureBox == pictureBox1)
                {
                    pictureBoxResult.Image = img2;
                }
                else if (targetPictureBox == pictureBox2)
                {
                    pictureBoxResult.Image = img2;
                }
            }
            else
            {
                MessageBox.Show("Por favor, insira um valor numérico válido para o limiar.", "Valor Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

       // booleano pra testar se a imagem está em escala de cinza
        bool IsGrayScale(Bitmap img)
        {
            for (int i = 0; i < img.Width; i++)
            {
                for (int j = 0; j < img.Height; j++)
                {
                    Color pixel = img.GetPixel(i, j);
                    if (pixel.R != pixel.G || pixel.G != pixel.B)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            // verificação de radio button
            if (!rdBtnImg1.Checked && !rdBtnImg2.Checked)
            {
                MessageBox.Show("Por favor, selecione uma PictureBox para aplicar o MIN.", "Nenhuma PictureBox Selecionada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Bitmap img1 = null;
            PictureBox targetPictureBox = null;

            // se rbPictureBox1 for marcado
            if (rdBtnImg1.Checked)
            {
                if (pictureBox1.Image != null)
                {
                    img1 = new Bitmap(pictureBox1.Image);
                    targetPictureBox = pictureBox1;
                }
                else
                {
                    MessageBox.Show("Por favor, carregue uma imagem na PictureBox 1!", "Imagem não encontrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else if (rdBtnImg2.Checked)
            {
                if (pictureBox2.Image != null)
                {
                    img1 = new Bitmap(pictureBox2.Image);
                    targetPictureBox = pictureBox2;
                }
                else
                {
                    MessageBox.Show("Por favor, carregue uma imagem na PictureBox 2!", "Imagem não encontrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (img1 != null)
            {
                int largura = img1.Width;
                int altura = img1.Height;

                Bitmap imgFiltrada = new Bitmap(largura, altura);

                for (int y = 1; y < altura - 1; y++) // Evita as bordas
                {
                    for (int x = 1; x < largura - 1; x++) // Evita as bordas
                    {
                        List<int> vizinhos = new List<int>();

                        for (int j = -1; j <= 1; j++)
                        {
                            for (int i = -1; i <= 1; i++)
                            {
                                Color pixel = img1.GetPixel(x + i, y + j);
                                vizinhos.Add(pixel.R);
                            }
                        }

                        int menorValor = vizinhos.Min();

                        imgFiltrada.SetPixel(x, y, Color.FromArgb(menorValor, menorValor, menorValor));
                    }
                }

                pictureBoxResult.Image = imgFiltrada;

                // Se quiser exibir o histograma da imagem filtrada no chart2, pode-se seguir o mesmo processo usado anteriormente.
            }
        }

        private void btnMax_Click(object sender, EventArgs e)
        {
            // verificação de radio button
            if (!rdBtnImg1.Checked && !rdBtnImg2.Checked)
            {
                MessageBox.Show("Por favor, selecione uma PictureBox para aplicar o MAX.", "Nenhuma PictureBox Selecionada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Bitmap img1 = null;
            PictureBox targetPictureBox = null;

            // se rbPictureBox1 for marcado
            if (rdBtnImg1.Checked)
            {
                if (pictureBox1.Image != null)
                {
                    img1 = new Bitmap(pictureBox1.Image);
                    targetPictureBox = pictureBox1;
                }
                else
                {
                    MessageBox.Show("Por favor, carregue uma imagem na PictureBox 1!", "Imagem não encontrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else if (rdBtnImg2.Checked)
            {
                if (pictureBox2.Image != null)
                {
                    img1 = new Bitmap(pictureBox2.Image);
                    targetPictureBox = pictureBox2;
                }
                else
                {
                    MessageBox.Show("Por favor, carregue uma imagem na PictureBox 2!", "Imagem não encontrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (img1 != null)
            {
                int largura = img1.Width;
                int altura = img1.Height;

                Bitmap imgFiltrada = new Bitmap(largura, altura);

                for (int y = 1; y < altura - 1; y++) // Evita as bordas
                {
                    for (int x = 1; x < largura - 1; x++) // Evita as bordas
                    {
                        List<int> vizinhos = new List<int>();

                        for (int j = -1; j <= 1; j++)
                        {
                            for (int i = -1; i <= 1; i++)
                            {
                                Color pixel = img1.GetPixel(x + i, y + j);
                                vizinhos.Add(pixel.R);
                            }
                        }

                        // Encontrar o menor valor de intensidade na vizinhança 3x3
                        int menorValor = vizinhos.Max();

                        // Definir o novo valor do pixel na imagem filtrada
                        imgFiltrada.SetPixel(x, y, Color.FromArgb(menorValor, menorValor, menorValor));
                    }
                }

                pictureBoxResult.Image = imgFiltrada;

                // Se quiser exibir o histograma da imagem filtrada no chart2, pode-se seguir o mesmo processo usado anteriormente.
            }
        }
        private void btnMean_Click(object sender, EventArgs e)
        {
            // verificação de radio button
            if (!rdBtnImg1.Checked && !rdBtnImg2.Checked)
            {
                MessageBox.Show("Por favor, selecione uma PictureBox para aplicar a MEDIA.", "Nenhuma PictureBox Selecionada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Bitmap img1 = null;
            PictureBox targetPictureBox = null;

            // se rbPictureBox1 for marcado
            if (rdBtnImg1.Checked)
            {
                if (pictureBox1.Image != null)
                {
                    img1 = new Bitmap(pictureBox1.Image);
                    targetPictureBox = pictureBox1;
                }
                else
                {
                    MessageBox.Show("Por favor, carregue uma imagem na PictureBox 1!", "Imagem não encontrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else if (rdBtnImg2.Checked)
            {
                if (pictureBox2.Image != null)
                {
                    img1 = new Bitmap(pictureBox2.Image);
                    targetPictureBox = pictureBox2;
                }
                else
                {
                    MessageBox.Show("Por favor, carregue uma imagem na PictureBox 2!", "Imagem não encontrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (img1 != null)
            {
                int largura = img1.Width;
                int altura = img1.Height;

                Bitmap imgFiltrada = new Bitmap(largura, altura);

                for (int y = 1; y < altura - 1; y++)
                {
                    for (int x = 1; x < largura - 1; x++)
                    {
                        int soma = 0;

                        for (int j = -1; j <= 1; j++)
                        {
                            for (int i = -1; i <= 1; i++)
                            {
                                Color pixel = img1.GetPixel(x + i, y + j);
                                soma += pixel.R;
                            }
                        }

                        // Calcula a média dos 9 valores
                        int media = soma / 9;

                        // Define o novo valor do pixel na imagem filtrada
                        imgFiltrada.SetPixel(x, y, Color.FromArgb(media, media, media));
                    }
                }

                pictureBoxResult.Image = imgFiltrada;

                // Se quiser exibir o histograma da imagem filtrada no chart2, pode-se seguir o mesmo processo usado anteriormente.
            }
        }

        private void btMediana_Click(object sender, EventArgs e)
        {
            // verificação de radio button
            if (!rbPictureBox11.Checked && !rbPictureBox22.Checked)
            {
                MessageBox.Show("Por favor, selecione uma PictureBox para aplicar a mediana.", "Nenhuma PictureBox Selecionada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Bitmap img1 = null;
            PictureBox targetPictureBox = null;

            // se rbPictureBox1 for marcado
            if (rbPictureBox11.Checked)
            {
                if (pictureBox1.Image != null)
                {
                    img1 = new Bitmap(pictureBox1.Image);
                    targetPictureBox = pictureBox1;
                }
                else
                {
                    MessageBox.Show("Por favor, carregue uma imagem na PictureBox 1!", "Imagem não encontrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else if (rbPictureBox22.Checked)
            {
                if (pictureBox2.Image != null)
                {
                    img1 = new Bitmap(pictureBox2.Image);
                    targetPictureBox = pictureBox2;
                }
                else
                {
                    MessageBox.Show("Por favor, carregue uma imagem na PictureBox 2!", "Imagem não encontrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (img1 != null)
            {
                int largura = img1.Width;
                int altura = img1.Height;

                Bitmap imgFiltrada = new Bitmap(largura, altura);

                for (int y = 1; y < altura - 1; y++)
                {
                    for (int x = 1; x < largura - 1; x++)
                    {
                        int[] r = new int[9];
                        int[] g = new int[9];
                        int[] b = new int[9];

                        int count = 0;

                        // Varre a matriz 3x3 ao redor do pixel (x, y)
                        for (int j = -1; j <= 1; j++)
                        {
                            for (int i = -1; i <= 1; i++)
                            {
                                Color pixel = img1.GetPixel(x + i, y + j);

                                r[count] = pixel.R;
                                g[count] = pixel.G;
                                b[count] = pixel.B;
                                count++;
                            }
                        }

                        Array.Sort(r);
                        Array.Sort(g);
                        Array.Sort(b);

                        int medianaR = r[4];
                        int medianaG = g[4];
                        int medianaB = b[4];

                        imgFiltrada.SetPixel(x, y, Color.FromArgb(medianaR, medianaG, medianaB));
                    }
                }

                pictureBoxResult.Image = imgFiltrada;
            }

        }

        private void btOrdem_Click(object sender, EventArgs e)
        {
            // verificação de radio button
            if (!rbPictureBox11.Checked && !rbPictureBox22.Checked)
            {
                MessageBox.Show("Por favor, selecione uma PictureBox para aplicar a ordem.", "Nenhuma PictureBox Selecionada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Bitmap img1 = null;
            PictureBox targetPictureBox = null;

            // se rbPictureBox1 for marcado
            if (rbPictureBox11.Checked)
            {
                if (pictureBox1.Image != null)
                {
                    img1 = new Bitmap(pictureBox1.Image);
                    targetPictureBox = pictureBox1;
                }
                else
                {
                    MessageBox.Show("Por favor, carregue uma imagem na PictureBox 1!", "Imagem não encontrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else if (rbPictureBox22.Checked)
            {
                if (pictureBox2.Image != null)
                {
                    img1 = new Bitmap(pictureBox2.Image);
                    targetPictureBox = pictureBox2;
                }
                else
                {
                    MessageBox.Show("Por favor, carregue uma imagem na PictureBox 2!", "Imagem não encontrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (img1 != null)
            {
                int largura = img1.Width;
                int altura = img1.Height;

                // Criar uma nova imagem para armazenar o resultado
                Bitmap imgFiltrada = new Bitmap(largura, altura);

                // Tenta converter o valor da TextBox para um número inteiro
                int ordem;
                bool entrada = int.TryParse(txSuav.Text, out ordem);

                // Verifica se a entrada é um número válido e está no intervalo entre 0 e 8
                if (!entrada || ordem < 0 || ordem > 8)
                {
                    MessageBox.Show("Por favor, insira um valor inteiro entre 0 e 8.");
                    return; // Interrompe a execução se a entrada não for válida
                }

                // Varrer a imagem pixel por pixel, exceto as bordas
                for (int y = 1; y < altura - 1; y++)
                {
                    for (int x = 1; x < largura - 1; x++)
                    {
                        // Lista para armazenar os valores dos pixels em cada canal de cor
                        int[] r = new int[9];
                        int[] g = new int[9];
                        int[] b = new int[9];

                        int count = 0;

                        // Varre a matriz 3x3 ao redor do pixel (x, y)
                        for (int j = -1; j <= 1; j++)
                        {
                            for (int i = -1; i <= 1; i++)
                            {
                                // Pega o pixel vizinho
                                Color pixel = img1.GetPixel(x + i, y + j);

                                // Armazena os valores de R, G e B
                                r[count] = pixel.R;
                                g[count] = pixel.G;
                                b[count] = pixel.B;
                                count++;
                            }
                        }

                        // Ordena os valores para aplicar a filtragem por ordem
                        Array.Sort(r);
                        Array.Sort(g);
                        Array.Sort(b);

                        // Obtém o valor correspondente à ordem escolhida
                        int ordemR = r[ordem];
                        int ordemG = g[ordem];
                        int ordemB = b[ordem];

                        // Define o novo valor do pixel na imagem filtrada com os valores ordenados
                        imgFiltrada.SetPixel(x, y, Color.FromArgb(ordemR, ordemG, ordemB));
                    }
                }

                pictureBoxResult.Image = imgFiltrada;
            }
        }

        private void btSuavizacao_Click(object sender, EventArgs e)
        {
            // verificação de radio button
            if (!rbPictureBox11.Checked && !rbPictureBox22.Checked)
            {
                MessageBox.Show("Por favor, selecione uma PictureBox para aplicar a suavização.", "Nenhuma PictureBox Selecionada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Bitmap img1 = null;
            PictureBox targetPictureBox = null;

            // se rbPictureBox1 for marcado
            if (rbPictureBox11.Checked)
            {
                if (pictureBox1.Image != null)
                {
                    img1 = new Bitmap(pictureBox1.Image);
                    targetPictureBox = pictureBox1;
                }
                else
                {
                    MessageBox.Show("Por favor, carregue uma imagem na PictureBox 1!", "Imagem não encontrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else if (rbPictureBox22.Checked)
            {
                if (pictureBox2.Image != null)
                {
                    img1 = new Bitmap(pictureBox2.Image);
                    targetPictureBox = pictureBox2;
                }
                else
                {
                    MessageBox.Show("Por favor, carregue uma imagem na PictureBox 2!", "Imagem não encontrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (img1 != null)
            {
                int largura = img1.Width;
                int altura = img1.Height;

                // Criar uma nova imagem para armazenar o resultado
                Bitmap imgFiltrada = new Bitmap(largura, altura);

                // Varrer a imagem pixel por pixel, exceto as bordas
                for (int y = 1; y < altura - 1; y++)
                {
                    for (int x = 1; x < largura - 1; x++)
                    {
                        // Listas para armazenar os valores dos pixels em cada canal de cor
                        int[] r = new int[9];
                        int[] g = new int[9];
                        int[] b = new int[9];

                        int count = 0;

                        // Varre a matriz 3x3 ao redor do pixel (x, y)
                        for (int j = -1; j <= 1; j++)
                        {
                            for (int i = -1; i <= 1; i++)
                            {
                                // Pega o pixel vizinho
                                Color pixel = img1.GetPixel(x + i, y + j);

                                // Armazena os valores de R, G e B
                                r[count] = pixel.R;
                                g[count] = pixel.G;
                                b[count] = pixel.B;
                                count++;
                            }
                        }

                        // Obtém o valor do pixel central
                        Color pixelCentral = img1.GetPixel(x, y);
                        int centralR = pixelCentral.R;
                        int centralG = pixelCentral.G;
                        int centralB = pixelCentral.B;

                        // Determina os valores mínimos e máximos dos pixels vizinhos
                        int minR = r.Min();
                        int maxR = r.Max();
                        int minG = g.Min();
                        int maxG = g.Max();
                        int minB = b.Min();
                        int maxB = b.Max();

                        // Calcula os novos valores para cada canal
                        int novoR = Math.Max(minR, Math.Min(centralR, maxR));
                        int novoG = Math.Max(minG, Math.Min(centralG, maxG));
                        int novoB = Math.Max(minB, Math.Min(centralB, maxB));

                        // Define o novo valor do pixel na imagem filtrada
                        imgFiltrada.SetPixel(x, y, Color.FromArgb(novoR, novoG, novoB));
                    }
                }

                // Atualiza a PictureBox com a imagem filtrada
                pictureBoxResult.Image = imgFiltrada;

            }
        }

        private byte[,] convBefore;
        private byte[,] convAfter;

        private void btGaussiano_Click(object sender, EventArgs e)
        {
            // verificação de radio button
            if (!rbPictureBox11.Checked && !rbPictureBox22.Checked)
            {
                MessageBox.Show("Por favor, selecione uma PictureBox para aplicar a suavização.", "Nenhuma PictureBox Selecionada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Bitmap img1 = null;
            PictureBox targetPictureBox = null;

            // se rbPictureBox1 for marcado
            if (rbPictureBox11.Checked)
            {
                if (pictureBox1.Image != null)
                {
                    img1 = new Bitmap(pictureBox1.Image);
                    targetPictureBox = pictureBox1;
                }
                else
                {
                    MessageBox.Show("Por favor, carregue uma imagem na PictureBox 1!", "Imagem não encontrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else if (rbPictureBox22.Checked)
            {
                if (pictureBox2.Image != null)
                {
                    img1 = new Bitmap(pictureBox2.Image);
                    targetPictureBox = pictureBox2;
                }
                else
                {
                    MessageBox.Show("Por favor, carregue uma imagem na PictureBox 2!", "Imagem não encontrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (txSuav.Text == "")
            {
                MessageBox.Show("Por favor, informe um valor entre 0 e 8!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                img1 = new Bitmap(img1.Width, img1.Height);
                convBefore = new byte[img1.Width, img1.Height];
                convAfter = new byte[img1.Width, img1.Height];

                double sigma = Convert.ToDouble(txSuav.Text);
                double[,] GKernel;
                GKernel = new double[5, 5];
                double sum = 0;
                double[,] mask = new double[5, 5];


                //gerar o kernel 5x5
                for (int x = -2; x <= 2; x++)
                {
                    for (int y = -2; y <= 2; y++)
                    {
                        double coefficient = 1.0f / (2.0f * Math.PI * sigma * sigma);
                        double exponent = -(x * x + y * y) / (2.0f * sigma * sigma);
                        GKernel[x + 2, y + 2] = coefficient * Math.Exp(exponent);

                        sum += GKernel[x + 2, y + 2];

                    }
                }

                //Imprime o Kerner gaussiano
                StreamWriter sw = new StreamWriter("GaussianKernel.txt");
                sw.WriteLine("Sigma: {0:f2}", sigma);
                sw.WriteLine("Sum: {0:f2}", sum);
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        sw.Write(String.Format("{0:f2}", GKernel[i, j]) + "\t");
                    }

                    sw.WriteLine(); //próxima linha
                }

                sw.Close();

                //normalizar o kernel
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        GKernel[i, j] /= sum;
                    }
                }

                // Percorre todos os pixels da imagem...
                for (int i = 0; i < img1.Width; i++)
                {
                    for (int j = 0; j < img1.Height; j++)
                    {
                        Color pixel = img1.GetPixel(i, j);

                        // Para imagens em escala de cinza, extrair o valor do pixel com...
                        byte pixelIntensity = Convert.ToByte((pixel.R + pixel.G + pixel.B) / 3);

                        convBefore[i, j] = pixelIntensity;

                    }
                }

                //aplicar o filtro
                for (int x = 2; x < img1.Width - 2; x++)
                {
                    for (int y = 2; y < img1.Height - 2; y++)
                    {


                        mask[0, 0] = (byte)(GKernel[0, 0] * convBefore[x - 2, y - 2]);
                        mask[0, 1] = (byte)(GKernel[0, 1] * convBefore[x - 2, y - 1]);
                        mask[0, 2] = (byte)(GKernel[0, 2] * convBefore[x - 2, y]);
                        mask[0, 3] = (byte)(GKernel[0, 3] * convBefore[x - 2, y + 1]);
                        mask[0, 4] = (byte)(GKernel[0, 4] * convBefore[x - 2, y + 2]);

                        mask[1, 0] = (byte)(GKernel[1, 0] * convBefore[x - 1, y - 2]);
                        mask[1, 1] = (byte)(GKernel[1, 1] * convBefore[x - 1, y - 1]);
                        mask[1, 2] = (byte)(GKernel[1, 2] * convBefore[x - 1, y]);
                        mask[1, 3] = (byte)(GKernel[1, 3] * convBefore[x - 1, y + 1]);
                        mask[1, 4] = (byte)(GKernel[1, 4] * convBefore[x - 1, y + 2]);

                        mask[2, 0] = (byte)(GKernel[2, 0] * convBefore[x, y - 2]);
                        mask[2, 1] = (byte)(GKernel[2, 1] * convBefore[x, y - 1]);
                        mask[2, 2] = (byte)(GKernel[2, 2] * convBefore[x, y]);
                        mask[2, 3] = (byte)(GKernel[2, 3] * convBefore[x, y + 1]);
                        mask[2, 4] = (byte)(GKernel[2, 4] * convBefore[x, y + 2]);

                        mask[3, 0] = (byte)(GKernel[3, 0] * convBefore[x + 1, y - 2]);
                        mask[3, 1] = (byte)(GKernel[3, 1] * convBefore[x + 1, y - 1]);
                        mask[3, 2] = (byte)(GKernel[3, 2] * convBefore[x + 1, y]);
                        mask[3, 3] = (byte)(GKernel[3, 3] * convBefore[x + 1, y + 1]);
                        mask[3, 4] = (byte)(GKernel[3, 4] * convBefore[x + 1, y + 2]);

                        mask[4, 0] = (byte)(GKernel[4, 0] * convBefore[x + 2, y - 2]);
                        mask[4, 1] = (byte)(GKernel[4, 1] * convBefore[x + 2, y - 1]);
                        mask[4, 2] = (byte)(GKernel[4, 2] * convBefore[x + 2, y]);
                        mask[4, 3] = (byte)(GKernel[4, 3] * convBefore[x + 2, y + 1]);
                        mask[4, 4] = (byte)(GKernel[4, 4] * convBefore[x + 2, y + 2]);


                        //Soma todos os elementos da vizinhança 
                        //A soma será o valor final do pixel

                        int pixel = 0;

                        for (int i = 0; i < 5; i++)
                        {
                            for (int j = 0; j < 5; j++)
                            {
                                pixel += (int)mask[i, j];
                            }
                        }

                        convAfter[x, y] = Convert.ToByte(pixel);

                        Color cor = Color.FromArgb(
                                255,
                                convAfter[x, y],
                                convAfter[x, y],
                                convAfter[x, y]);

                        img1.SetPixel(x, y, cor);

                    }
                }
                pictureBoxResult.Image = img1;
            }
        }

            private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
