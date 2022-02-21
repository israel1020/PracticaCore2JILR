using PracticaCore2JILR.Data;
using PracticaCore2JILR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticaCore2JILR.Repositories
{
    public class RepositoryLibros
    {
        private LibrosContext context;
        public RepositoryLibros(LibrosContext context)
        {
            this.context = context;
        }
        public List<Libro> GetLibros()
        {
            return this.context.Libros.ToList();
        }
        public Libro FindLibro(int idlibro)
        {
            return this.context.Libros.FirstOrDefault(x => x.IdLiBRO == idlibro);
        }
        public List<Genero> GetGeneros()
        {
            return this.context.Generos.ToList();
        }
        public List<Libro> GetLibrosGenero(int idgenero)
        {
            var consulta = from datos in this.context.Libros
                           where datos.IdGenero == idgenero
                           select datos;
            return consulta.ToList();
        }
        public Usuario FindUsuario(string email, string password)
        {
            //var consulta = from d in this.context.Usuarios
            //               where d.Email == email
            //               && d.Password ==password
            //               select d;
            //return consulta.FirstOrDefault();
            return this.context.Usuarios.FirstOrDefault(
                x => x.Email == email && x.Password == password);
        }
        public List<Libro> GetLibrosSesion(List<int> idsLibros)
        {
            var consulta = from datos in this.context.Libros
                           where idsLibros.Contains(datos.IdLiBRO)
                           select datos;
            if(consulta.Count() == 0)
            {
                return null;
            }
            return consulta.ToList();
        }
    }
}
