using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
                // Obtém as dimensões da imagem
                Bitmap img1 = new Bitmap(pictureBox1.Image);
                int width = img1.Width;
                int height = img1.Height;

                // Cria uma nova imagem para o resultado
                Bitmap resultado = new Bitmap(width, height);

                // Verifica se a TextBox está vazia
                if (string.IsNullOrWhiteSpace(txImg1.Text))
                {
                    MessageBox.Show("Por favor, insira um valor para o brilho.", "Erro de Entrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int txValor;
                try
                {
                    txValor = Convert.ToInt32(txImg1.Text);
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

                // Valida o valor do brilho
                if (txValor < 0)
                {
                    MessageBox.Show("Valor de brilho inválido. O valor deve ser maior ou igual a 0!", "Erro de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (txValor > 255)
                {
                    MessageBox.Show("Valor de brilho inválido. O valor deve ser menor ou igual a 255!", "Erro de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Aplica o brilho
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
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Erro inesperado: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }

                // Exibe o resultado
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
                // Obtém as dimensões da imagem
                Bitmap img1 = new Bitmap(pictureBox1.Image);
                int width = img1.Width;
                int height = img1.Height;

                // Cria uma nova imagem para o resultado
                Bitmap resultado = new Bitmap(width, height);

                // Verifica se a TextBox está vazia
                if (string.IsNullOrWhiteSpace(txImg1.Text))
                {
                    MessageBox.Show("Por favor, insira um valor para o brilho.", "Erro de Entrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int txValor;
                try
                {
                    // Converte o valor da TextBox para inteiro
                    txValor = Convert.ToInt32(txImg1.Text);
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

                // Valida o valor do brilho
                if (txValor < 0)
                {
                    MessageBox.Show("Valor de brilho inválido. O valor deve ser maior ou igual a 0!", "Erro de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (txValor > 255)
                {
                    MessageBox.Show("Valor de brilho inválido. O valor deve ser menor ou igual a 255!", "Erro de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Aplica a subtração do brilho na imagem
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        Color cor1 = img1.GetPixel(x, y);

                        try
                        {
                            // Subtrai os valores de cor e aplica o limite de 0 (não permite valores negativos)
                            int r = Math.Max(cor1.R - txValor, 0);
                            int g = Math.Max(cor1.G - txValor, 0);
                            int b = Math.Max(cor1.B - txValor, 0);

                            // Define a cor resultante na nova imagem
                            resultado.SetPixel(x, y, Color.FromArgb(r, g, b));
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Erro inesperado: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }

                // Exibe o resultado
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
                // Obtém as dimensões da imagem
                Bitmap img1 = new Bitmap(pictureBox2.Image);
                int width = img1.Width;
                int height = img1.Height;

                // Cria uma nova imagem para o resultado
                Bitmap resultado = new Bitmap(width, height);

                // Verifica se a TextBox está vazia
                if (string.IsNullOrWhiteSpace(txImg2.Text))
                {
                    MessageBox.Show("Por favor, insira um valor para o brilho.", "Erro de Entrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int txValor;
                try
                {
                    // Converte o valor da TextBox para inteiro
                    txValor = Convert.ToInt32(txImg2.Text);
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

                // Valida o valor do brilho
                if (txValor < 0)
                {
                    MessageBox.Show("Valor de brilho inválido. O valor deve ser maior ou igual a 0!", "Erro de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (txValor > 255)
                {
                    MessageBox.Show("Valor de brilho inválido. O valor deve ser menor ou igual a 255!", "Erro de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Aplica o brilho na imagem
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
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Erro inesperado: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }

                // Exibe o resultado
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
                // Obtém as dimensões da imagem
                Bitmap img1 = new Bitmap(pictureBox2.Image);
                int width = img1.Width;
                int height = img1.Height;

                // Cria uma nova imagem para o resultado
                Bitmap resultado = new Bitmap(width, height);

                // Verifica se a TextBox está vazia
                if (string.IsNullOrWhiteSpace(txImg2.Text))
                {
                    MessageBox.Show("Por favor, insira um valor para o brilho.", "Erro de Entrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int txValor;
                try
                {
                    // Converte o valor da TextBox para inteiro
                    txValor = Convert.ToInt32(txImg2.Text);
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

                // Valida o valor do brilho
                if (txValor < 0)
                {
                    MessageBox.Show("Valor de brilho inválido. O valor deve ser maior ou igual a 0!", "Erro de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (txValor > 255)
                {
                    MessageBox.Show("Valor de brilho inválido. O valor deve ser menor ou igual a 255!", "Erro de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Aplica a subtração do brilho na imagem
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        Color cor1 = img1.GetPixel(x, y);

                        try
                        {
                            // Subtrai os valores de cor e aplica o limite de 0 (não permite valores negativos)
                            int r = Math.Max(cor1.R - txValor, 0);
                            int g = Math.Max(cor1.G - txValor, 0);
                            int b = Math.Max(cor1.B - txValor, 0);

                            // Define a cor resultante na nova imagem
                            resultado.SetPixel(x, y, Color.FromArgb(r, g, b));
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Erro inesperado: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }

                // Exibe o resultado
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
            // Verificação de radio button
            if (!rbPictureBox11.Checked && !rbPictureBox22.Checked)
            {
                MessageBox.Show("Por favor, selecione uma PictureBox para aplicar a ordem.", "Nenhuma PictureBox Selecionada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Bitmap img1 = null;
            PictureBox targetPictureBox = null;

            // Se rbPictureBox1 for marcado
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

                int ordem;
                bool entrada = int.TryParse(txSuav.Text, out ordem);

                // verificação do valor de entrada
                if (!entrada || ordem < 1 || ordem > 9)
                {
                    MessageBox.Show("Por favor, insira um valor inteiro entre 1 e 9.");
                    return; 
                }

                // definir a vizinhança
                int tamanhoVizinhança = 3;
                int tamanhoMatriz = tamanhoVizinhança * tamanhoVizinhança; 

   
                for (int y = 1; y < altura - 1; y++)
                {
                    for (int x = 1; x < largura - 1; x++)
                    {
                        int[] r = new int[tamanhoMatriz];
                        int[] g = new int[tamanhoMatriz];
                        int[] b = new int[tamanhoMatriz];

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

                        Array.Sort(r);
                        Array.Sort(g);
                        Array.Sort(b);

                        int ordemR = r[ordem - 1]; // a ordem é 1-based, então subtrai-se 1 para acessar o índice correto
                        int ordemG = g[ordem - 1];
                        int ordemB = b[ordem - 1];

                        imgFiltrada.SetPixel(x, y, Color.FromArgb(ordemR, ordemG, ordemB));
                    }
                }
                pictureBoxResult.Image = imgFiltrada;
            }
        }

        private void btSuavizacao_Click(object sender, EventArgs e)
        {
            // Verificação de radio button
            if (!rbPictureBox11.Checked && !rbPictureBox22.Checked)
            {
                MessageBox.Show("Por favor, selecione uma PictureBox para aplicar a suavização.", "Nenhuma PictureBox Selecionada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Bitmap img1 = null;
            PictureBox targetPictureBox = null;

            // Se rbPictureBox1 for marcado
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
                        int[] r = new int[8];
                        int[] g = new int[8];
                        int[] b = new int[8];

                        int count = 0;

                        // Varre a matriz 3x3 ao redor do pixel (x, y), excluindo o próprio pixel central
                        for (int j = -1; j <= 1; j++)
                        {
                            for (int i = -1; i <= 1; i++)
                            {
                                if (i == 0 && j == 0) continue; // Ignora o próprio pixel central

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

                        // Calcula os novos valores para cada canal de cor de forma conservativa
                        int novoR = centralR;
                        int novoG = centralG;
                        int novoB = centralB;

                        // Comparação e ajuste do valor do pixel
                        if (centralR < minR)
                            novoR = minR;
                        else if (centralR > maxR)
                            novoR = maxR;

                        if (centralG < minG)
                            novoG = minG;
                        else if (centralG > maxG)
                            novoG = maxG;

                        if (centralB < minB)
                            novoB = minB;
                        else if (centralB > maxB)
                            novoB = maxB;

                        // Define o novo valor do pixel na imagem filtrada
                        imgFiltrada.SetPixel(x, y, Color.FromArgb(novoR, novoG, novoB));
                    }
                }

                // Atualiza a PictureBox com a imagem filtrada
                pictureBoxResult.Image = imgFiltrada;
            }
        }


        private int[,] convBefore;
        private int[,] convAfter;

        private void btGaussiano_Click(object sender, EventArgs e)
        {
              if (pictureBox1.Image == null)
            {
                MessageBox.Show("Por favor, selecione uma imagem!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (txSuav.Text == "")
                {
                    MessageBox.Show("Por favor, informe um valor entre 0 e 8!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    img1 = new Bitmap(pictureBox1.Image);
                    convBefore = new int[img1.Width, img1.Height];  
                    convAfter = new int[img1.Width, img1.Height];   

                    double sigma = Convert.ToDouble(txSuav.Text);
                    double[,] GKernel = new double[5, 5];
                    double sum = 0;

                    // Gerar o kernel 5x5
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

                    // Normalizar o kernel
                    for (int i = 0; i < 5; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            GKernel[i, j] /= sum;
                        }
                    }

                    // Percorrer todos os pixels da imagem
                    for (int i = 0; i < img1.Width; i++)
                    {
                        for (int j = 0; j < img1.Height; j++)
                        {
                            Color pixel = img1.GetPixel(i, j);
                            // Para imagens em escala de cinza, extrair o valor do pixel
                            int pixelIntensity = (pixel.R + pixel.G + pixel.B) / 3;
                            convBefore[i, j] = pixelIntensity;
                        }
                    }

                    // Aplicar o filtro Gaussiano
                    for (int x = 2; x < img1.Width - 2; x++)
                    { 
                        for (int y = 2; y < img1.Height - 2; y++)
                        {
                            int pixel = 0;

                            // Aplicar o kernel sobre o pixel
                            for (int i = 0; i < 5; i++)
                            {
                                for (int j = 0; j < 5; j++)
                                {
                                    pixel += (int)(GKernel[i, j] * convBefore[x - 2 + i, y - 2 + j]);
                                }
                            }


                            convAfter[x, y] = pixel;

                            Color cor = Color.FromArgb(255, convAfter[x, y], convAfter[x, y], convAfter[x, y]);
                            img1.SetPixel(x, y, cor);
                        }
                    }

                    pictureBoxResult.Image = img1;
                }
            }
        }


        private void btnPrewitt_Click(object sender, EventArgs e)
        {
            // Verificação de radio button
            if (!rbBordas1.Checked && !rbBordas2.Checked)
            {
                MessageBox.Show("Por favor, selecione uma PictureBox para aplicar o filtro de Prewitt.", "Nenhuma PictureBox Selecionada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Bitmap img1 = null;
            PictureBox targetPictureBox = null;

            // Se rbPictureBox1 for marcado
            if (rbBordas1.Checked)
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
            else if (rbBordas2.Checked)
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

                // Definindo os kernels de Prewitt
                int[,] Gx = new int[3, 3]
                {
            { -1, 0, 1 },
            { -1, 0, 1 },
            { -1, 0, 1 }
                };

                int[,] Gy = new int[3, 3]
                {
            { -1, -1, -1 },
            {  0,  0,  0 },
            {  1,  1,  1 }
                };

                // Varrer a imagem pixel por pixel
                for (int y = 1; y < altura - 1; y++)
                {
                    for (int x = 1; x < largura - 1; x++)
                    {
                        int gradienteX_R = 0, gradienteX_G = 0, gradienteX_B = 0;
                        int gradienteY_R = 0, gradienteY_G = 0, gradienteY_B = 0;

                        // Convolução para Gx (derivada no eixo X)
                        for (int j = -1; j <= 1; j++)
                        {
                            for (int i = -1; i <= 1; i++)
                            {
                                Color pixel = img1.GetPixel(x + i, y + j);

                                gradienteX_R += Gx[j + 1, i + 1] * pixel.R;
                                gradienteX_G += Gx[j + 1, i + 1] * pixel.G;
                                gradienteX_B += Gx[j + 1, i + 1] * pixel.B;

                                gradienteY_R += Gy[j + 1, i + 1] * pixel.R;
                                gradienteY_G += Gy[j + 1, i + 1] * pixel.G;
                                gradienteY_B += Gy[j + 1, i + 1] * pixel.B;
                            }
                        }

                        // Calcular a magnitude do gradiente para cada canal de cor
                        int gradiente_R = (int)Math.Min(255, Math.Sqrt(gradienteX_R * gradienteX_R + gradienteY_R * gradienteY_R));
                        int gradiente_G = (int)Math.Min(255, Math.Sqrt(gradienteX_G * gradienteX_G + gradienteY_G * gradienteY_G));
                        int gradiente_B = (int)Math.Min(255, Math.Sqrt(gradienteX_B * gradienteX_B + gradienteY_B * gradienteY_B));

                        // Definir o novo valor do pixel na imagem filtrada
                        imgFiltrada.SetPixel(x, y, Color.FromArgb(gradiente_R, gradiente_G, gradiente_B));
                    }
                }

                // Atualizar a PictureBox com a imagem resultante
                pictureBoxResult.Image = imgFiltrada;
            }
        }

        private void btnSobel_Click(object sender, EventArgs e)
        {
            // Verificação de radio button
            if (!rbBordas1.Checked && !rbBordas2.Checked)
            {
                MessageBox.Show("Por favor, selecione uma PictureBox para aplicar o filtro Sobel.", "Nenhuma PictureBox Selecionada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Bitmap img1 = null;
            PictureBox targetPictureBox = null;

            // Se rbPictureBox1 for marcado
            if (rbBordas1.Checked)
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
            else if (rbBordas2.Checked)
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

                // kernel de sobel
                int[,] Gx = new int[3, 3]
                {
            { -1,  0,  1 },
            { -2,  0,  2 },
            { -1,  0,  1 }
                };

                int[,] Gy = new int[3, 3]
                {
            { -1, -2, -1 },
            {  0,  0,  0 },
            {  1,  2,  1 }
                };

                for (int y = 1; y < altura - 1; y++)
                {
                    for (int x = 1; x < largura - 1; x++)
                    {
                        int gradienteX_R = 0, gradienteX_G = 0, gradienteX_B = 0;
                        int gradienteY_R = 0, gradienteY_G = 0, gradienteY_B = 0;

                        // convolução para Gx e Gy 
                        for (int j = -1; j <= 1; j++)
                        {
                            for (int i = -1; i <= 1; i++)
                            {
                                Color pixel = img1.GetPixel(x + i, y + j);

                                gradienteX_R += Gx[j + 1, i + 1] * pixel.R;
                                gradienteX_G += Gx[j + 1, i + 1] * pixel.G;
                                gradienteX_B += Gx[j + 1, i + 1] * pixel.B;

                                gradienteY_R += Gy[j + 1, i + 1] * pixel.R;
                                gradienteY_G += Gy[j + 1, i + 1] * pixel.G;
                                gradienteY_B += Gy[j + 1, i + 1] * pixel.B;
                            }
                        }

                        // Calcular a magnitude do gradiente para cada canal de cor
                        int gradiente_R = (int)Math.Min(255, Math.Sqrt(gradienteX_R * gradienteX_R + gradienteY_R * gradienteY_R));
                        int gradiente_G = (int)Math.Min(255, Math.Sqrt(gradienteX_G * gradienteX_G + gradienteY_G * gradienteY_G));
                        int gradiente_B = (int)Math.Min(255, Math.Sqrt(gradienteX_B * gradienteX_B + gradienteY_B * gradienteY_B));

                        // Definir o novo valor do pixel na imagem filtrada
                        imgFiltrada.SetPixel(x, y, Color.FromArgb(gradiente_R, gradiente_G, gradiente_B));
                    }
                }

                // Atualizar a PictureBox com a imagem resultante
                pictureBoxResult.Image = imgFiltrada;
            }
        }
        private void btnLaplaciano_Click(object sender, EventArgs e)
        {
            // Verificação de radio button
            if (!rbBordas1.Checked && !rbBordas2.Checked)
            {
                MessageBox.Show("Por favor, selecione uma PictureBox para aplicar o filtro Laplaciano.", "Nenhuma PictureBox Selecionada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Bitmap img1 = null;
            PictureBox targetPictureBox = null;

            // Se rbPictureBox1 for marcado
            if (rbBordas1.Checked)
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
            else if (rbBordas2.Checked)
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

                // Definindo o kernel de Laplaciano
                int[,] kernel = new int[3, 3]
                {
            {  0,  1,  0 },
            {  1, -4,  1 },
            {  0,  1,  0 }
                };

                // Varrer a imagem pixel por pixel, ignorando as bordas (pixels da primeira e última linha/coluna)
                for (int y = 1; y < altura - 1; y++)
                {
                    for (int x = 1; x < largura - 1; x++)
                    {
                        int somaR = 0, somaG = 0, somaB = 0;

                        // Convolução do kernel com a imagem
                        for (int j = -1; j <= 1; j++)
                        {
                            for (int i = -1; i <= 1; i++)
                            {
                                Color pixel = img1.GetPixel(x + i, y + j);

                                somaR += kernel[j + 1, i + 1] * pixel.R;
                                somaG += kernel[j + 1, i + 1] * pixel.G;
                                somaB += kernel[j + 1, i + 1] * pixel.B;
                            }
                        }

                        // A intensidade resultante (magnitude) do pixel filtrado
                        int newR = Math.Min(255, Math.Max(0, Math.Abs(somaR)));
                        int newG = Math.Min(255, Math.Max(0, Math.Abs(somaG)));
                        int newB = Math.Min(255, Math.Max(0, Math.Abs(somaB)));

                        // Definir o novo valor do pixel filtrado
                        imgFiltrada.SetPixel(x, y, Color.FromArgb(newR, newG, newB));
                    }
                }

                // Atualizar a PictureBox com a imagem filtrada
                pictureBoxResult.Image = imgFiltrada;
            }
        }

        private void btnDilat_Click(object sender, EventArgs e)
        {
            // Verificação de radio button
            if (!rbMorf1.Checked && !rbMorf2.Checked)
            {
                MessageBox.Show("Por favor, selecione uma PictureBox para aplicar a dilatação.", "Nenhuma PictureBox Selecionada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Bitmap img1 = null;
            PictureBox targetPictureBox = null;

            if (rbMorf1.Checked)
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
            else if (rbMorf2.Checked)
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

                Bitmap imgDilatada = new Bitmap(largura, altura);

                for (int y = 1; y < altura - 1; y++)
                {
                    for (int x = 1; x < largura - 1; x++)
                    {
                        int maxR = 0, maxG = 0, maxB = 0;

                        for (int j = -1; j <= 1; j++)
                        {
                            for (int i = -1; i <= 1; i++)
                            {
                                Color pixel = img1.GetPixel(x + i, y + j);

                                maxR = Math.Max(maxR, pixel.R);
                                maxG = Math.Max(maxG, pixel.G);
                                maxB = Math.Max(maxB, pixel.B);
                            }
                        }

                        // valor maximo de cada canal
                        imgDilatada.SetPixel(x, y, Color.FromArgb(maxR, maxG, maxB));
                    }
                }

                pictureBoxResult.Image = imgDilatada;
            }
        }

        private void btnAbertura_Click(object sender, EventArgs e)
        {
            // Verificação de radio button
            if (!rbMorf1.Checked && !rbMorf2.Checked)
            {
                MessageBox.Show("Por favor, selecione uma PictureBox para aplicar a abertura.", "Nenhuma PictureBox Selecionada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Bitmap img1 = null;
            PictureBox targetPictureBox = null;

            if (rbMorf1.Checked)
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
            else if (rbMorf2.Checked)
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

                Bitmap imgAbertura = new Bitmap(largura, altura);

                // realiza a erosão primeiramente
                Bitmap imgErosao = new Bitmap(largura, altura);
                for (int y = 1; y < altura - 1; y++)
                {
                    for (int x = 1; x < largura - 1; x++)
                    {
                        int minR = 255, minG = 255, minB = 255;

                        for (int j = -1; j <= 1; j++)
                        {
                            for (int i = -1; i <= 1; i++)
                            {
                                Color pixel = img1.GetPixel(x + i, y + j);

                                minR = Math.Min(minR, pixel.R);
                                minG = Math.Min(minG, pixel.G);
                                minB = Math.Min(minB, pixel.B);
                            }
                        }

                        // valor minimo de cada canal
                        imgErosao.SetPixel(x, y, Color.FromArgb(minR, minG, minB));
                    }
                }

                // aplica dilatação na imagem da erosão
                for (int y = 1; y < altura - 1; y++)
                {
                    for (int x = 1; x < largura - 1; x++)
                    {
                        int maxR = 0, maxG = 0, maxB = 0;

                        for (int j = -1; j <= 1; j++)
                        {
                            for (int i = -1; i <= 1; i++)
                            {
                                Color pixel = imgErosao.GetPixel(x + i, y + j);

                                maxR = Math.Max(maxR, pixel.R);
                                maxG = Math.Max(maxG, pixel.G);
                                maxB = Math.Max(maxB, pixel.B);
                            }
                        }

                        // valor maximo de cada canal
                        imgAbertura.SetPixel(x, y, Color.FromArgb(maxR, maxG, maxB));
                    }
                }

                pictureBoxResult.Image = imgAbertura;
            }
        }

        private void btnFechamento_Click(object sender, EventArgs e)
        {
            // Verificação de radio button
            if (!rbMorf1.Checked && !rbMorf2.Checked)
            {
                MessageBox.Show("Por favor, selecione uma PictureBox para aplicar o fechamento.", "Nenhuma PictureBox Selecionada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Bitmap img1 = null;
            PictureBox targetPictureBox = null;

            if (rbMorf1.Checked)
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
            else if (rbMorf2.Checked)
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

                Bitmap imgFechamento = new Bitmap(largura, altura);

                // dilatação
                Bitmap imgDilatada = new Bitmap(largura, altura);
                for (int y = 1; y < altura - 1; y++)
                {
                    for (int x = 1; x < largura - 1; x++)
                    {
                        int maxR = 0, maxG = 0, maxB = 0;

                        for (int j = -1; j <= 1; j++)
                        {
                            for (int i = -1; i <= 1; i++)
                            {
                                Color pixel = img1.GetPixel(x + i, y + j);

                                maxR = Math.Max(maxR, pixel.R);
                                maxG = Math.Max(maxG, pixel.G);
                                maxB = Math.Max(maxB, pixel.B);
                            }
                        }

                        // valor maximo de cada canal de cor
                        imgDilatada.SetPixel(x, y, Color.FromArgb(maxR, maxG, maxB));
                    }
                }

                // aplica erosão após a dilatação
                for (int y = 1; y < altura - 1; y++)
                {
                    for (int x = 1; x < largura - 1; x++)
                    {
                        int minR = 255, minG = 255, minB = 255;

                        for (int j = -1; j <= 1; j++)
                        {
                            for (int i = -1; i <= 1; i++)
                            {
                                Color pixel = imgDilatada.GetPixel(x + i, y + j);

                                minR = Math.Min(minR, pixel.R);
                                minG = Math.Min(minG, pixel.G);
                                minB = Math.Min(minB, pixel.B);
                            }
                        }

                        // valor minimo de cada canal
                        imgFechamento.SetPixel(x, y, Color.FromArgb(minR, minG, minB));
                    }
                }

                // Atualizar a PictureBox com a imagem resultante do fechamento
                pictureBoxResult.Image = imgFechamento;
            }
        }

        private void btnContorno_Click(object sender, EventArgs e)
        {
            // Verificação de radio button
            if (!rbMorf1.Checked && !rbMorf2.Checked)
            {
                MessageBox.Show("Por favor, selecione uma PictureBox para aplicar o contorno.", "Nenhuma PictureBox Selecionada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Bitmap img1 = null;
            PictureBox targetPictureBox = null;

            if (rbMorf1.Checked)
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
            else if (rbMorf2.Checked)
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

                Bitmap imgContorno = new Bitmap(largura, altura);

                // kernel de sobel para bordas
                int[,] kernelX = new int[3, 3]
                {
            { -1, 0, 1 },
            { -2, 0, 2 },
            { -1, 0, 1 }
                };

                int[,] kernelY = new int[3, 3]
                {
            { -1, -2, -1 },
            { 0, 0, 0 },
            { 1, 2, 1 }
                };

                for (int y = 1; y < altura - 1; y++)
                {
                    for (int x = 1; x < largura - 1; x++)
                    {
                        int somaXr = 0, somaXg = 0, somaXb = 0;
                        int somaYr = 0, somaYg = 0, somaYb = 0;

                        for (int j = -1; j <= 1; j++)
                        {
                            for (int i = -1; i <= 1; i++)
                            {
                                Color pixel = img1.GetPixel(x + i, y + j);

                                // kernelX
                                somaXr += kernelX[j + 1, i + 1] * pixel.R;
                                somaXg += kernelX[j + 1, i + 1] * pixel.G;
                                somaXb += kernelX[j + 1, i + 1] * pixel.B;

                                // kernelY
                                somaYr += kernelY[j + 1, i + 1] * pixel.R;
                                somaYg += kernelY[j + 1, i + 1] * pixel.G;
                                somaYb += kernelY[j + 1, i + 1] * pixel.B;
                            }
                        }

                        // calcular o gradiente para cada canal de cor
                        int magnitudeR = Math.Min(255, (int)Math.Sqrt(somaXr * somaXr + somaYr * somaYr));
                        int magnitudeG = Math.Min(255, (int)Math.Sqrt(somaXg * somaXg + somaYg * somaYg));
                        int magnitudeB = Math.Min(255, (int)Math.Sqrt(somaXb * somaXb + somaYb * somaYb));

                        imgContorno.SetPixel(x, y, Color.FromArgb(magnitudeR, magnitudeG, magnitudeB));
                    }
                }

                pictureBoxResult.Image = imgContorno;
            }
        }

        private void btnErosao_Click(object sender, EventArgs e)
        {
            // Verificação de radio button
            if (!rbMorf1.Checked && !rbMorf2.Checked)
            {
                MessageBox.Show("Por favor, selecione uma PictureBox para aplicar a erosão.", "Nenhuma PictureBox Selecionada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Bitmap img1 = null;
            PictureBox targetPictureBox = null;

            // Se rbMorf1 for marcado
            if (rbMorf1.Checked)
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
            else if (rbMorf2.Checked)
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

                Bitmap imgErosao = new Bitmap(largura, altura);

                for (int y = 1; y < altura - 1; y++)
                {
                    for (int x = 1; x < largura - 1; x++)
                    {
                        int minR = 255, minG = 255, minB = 255;

                        // verificar os 8 vizinhos e o pixel
                        for (int j = -1; j <= 1; j++)
                        {
                            for (int i = -1; i <= 1; i++)
                            {
                                Color pixel = img1.GetPixel(x + i, y + j);

                                minR = Math.Min(minR, pixel.R);
                                minG = Math.Min(minG, pixel.G);
                                minB = Math.Min(minB, pixel.B);
                            }
                        }

                        imgErosao.SetPixel(x, y, Color.FromArgb(minR, minG, minB));
                    }
                }

                pictureBoxResult.Image = imgErosao;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
