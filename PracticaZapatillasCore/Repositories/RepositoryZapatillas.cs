using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PracticaZapatillasCore.Data;
using PracticaZapatillasCore.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.Mime.MediaTypeNames;

namespace PracticaZapatillasCore.Repositories
{
    #region PROCEDURES
    //CREATE PROCEDURE SP_ZAPATILLAS_PAGINACION
    //(@idprod INT, @posicion INT, @nroRegistros INT OUT)
    //AS
    //    select @nroRegistros = cast(COUNT(IDIMAGEN) as int)
    //                           from IMAGENESZAPASPRACTICA
    //                           where IDPRODUCTO = @idprod

    //    select IDIMAGEN, IDPRODUCTO, IMAGEN
    //    from(
    //        select cast(row_number() over (order by IdImagen) as int) as POSICION, IDIMAGEN, IDPRODUCTO, IMAGEN
    //        from IMAGENESZAPASPRACTICA
    //        where IDPRODUCTO = @idprod
	   // ) Query
    //    where POSICION = @posicion
    //GO

    #endregion

    public class RepositoryZapatillas
    {
        private ZapatillasContext context;

        public RepositoryZapatillas(ZapatillasContext context)
        {
            this.context = context;
        }

        public async Task<List<Zapatilla>> GetZapatillasAsync()
        {
            var consulta = await this.context.Zapatillas.ToListAsync();
            return consulta;
        }

        public async Task<Zapatilla> FindZapatillaAsync(int idprod)
        {
            var consulta = await this.context.Zapatillas
                                             .Where(x => x.IdProducto == idprod)
                                             .ToListAsync();  
            return consulta.FirstOrDefault();
        }

        public async Task<ImagenDTO> GetPaginacionImagenZapatillaAsync(int idprod, int pos)
        {
            string query = "EXEC SP_ZAPATILLAS_PAGINACION @idprod, @posicion, @nroRegistros OUT";

            SqlParameter paramSalida = new SqlParameter("@nroRegistros", 0);
            paramSalida.Direction = System.Data.ParameterDirection.Output;

            var consulta = await this.context.ImagenesZapatillas
                                             .FromSqlRaw(
                                                query,
                                                new SqlParameter("@idprod", idprod),
                                                new SqlParameter("@posicion", pos),
                                                paramSalida
                                             )
                                            .ToListAsync();

            var img = consulta.FirstOrDefault();
            int registros = int.Parse(paramSalida.Value.ToString());

            ImagenDTO datos = new ImagenDTO
            {
                Imagen = img,
                NroRegistros = registros
            };

            return datos;
        }
    }
}
