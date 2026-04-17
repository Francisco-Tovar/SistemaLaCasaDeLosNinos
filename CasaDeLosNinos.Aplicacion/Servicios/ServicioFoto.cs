using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using CasaDeLosNinos.Dominio.Interfaces;

namespace CasaDeLosNinos.Aplicacion.Servicios
{
    public class ServicioFoto : IServicioFoto
    {
        private readonly IRepositorioFoto _repositorioFoto;
        private const int MaxDimension = 400; // Resolución optimizada (400px)

        public ServicioFoto(IRepositorioFoto repositorioFoto)
        {
            _repositorioFoto = repositorioFoto;
        }

        public async Task<byte[]?> ObtenerFotoAsync(int idNino)
        {
            return await _repositorioFoto.ObtenerFotoAsync(idNino);
        }

        public async Task<bool> GuardarFotoAsync(int idNino, byte[] imagenOriginal)
        {
            try
            {
                byte[] imagenOptimizada = OptimizarImagen(imagenOriginal);
                await _repositorioFoto.GuardarFotoAsync(idNino, imagenOptimizada);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task EliminarFotoAsync(int idNino)
        {
            await _repositorioFoto.EliminarFotoAsync(idNino);
        }

        /// <summary>
        /// Redimensiona la imagen a un máximo de 400px y la comprime en formato JPG.
        /// </summary>
        private byte[] OptimizarImagen(byte[] imagenOriginal)
        {
            using var msIn = new MemoryStream(imagenOriginal);
            using var img = Image.FromStream(msIn);

            // Calcular nuevas dimensiones manteniendo la proporción
            int width = img.Width;
            int height = img.Height;

            if (width > height)
            {
                if (width > MaxDimension)
                {
                    height = (int)(height * ((float)MaxDimension / width));
                    width = MaxDimension;
                }
            }
            else
            {
                if (height > MaxDimension)
                {
                    width = (int)(width * ((float)MaxDimension / height));
                    height = MaxDimension;
                }
            }

            using var resampled = new Bitmap(width, height);
            using var g = Graphics.FromImage(resampled);
            
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.CompositingQuality = CompositingQuality.HighQuality;

            g.DrawImage(img, 0, 0, width, height);

            using var msOut = new MemoryStream();
            
            // Configurar compresión JPG al 75%
            var encoder = GetEncoder(ImageFormat.Jpeg);
            var parameters = new EncoderParameters(1);
            parameters.Param[0] = new EncoderParameter(Encoder.Quality, 75L);

            resampled.Save(msOut, encoder, parameters);
            return msOut.ToArray();
        }

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            var codecs = ImageCodecInfo.GetImageEncoders();
            foreach (var codec in codecs)
            {
                if (codec.FormatID == format.Guid) return codec;
            }
            return null!;
        }
    }
}
