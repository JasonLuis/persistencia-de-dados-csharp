using ScreenSound.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenSound.Banco
{
    internal class MusicaDAL
    {
        private readonly ScreenSoundContext context;

        public MusicaDAL(ScreenSoundContext context)
        {
            this.context = context; 
        }

        public IEnumerable<Musica> Listar ()
        {
            return context.Musicas.ToList();
        }

        public void Adicionar (Musica musica)
        {
            context.Musicas.Add(musica);
            context.SaveChanges();
        }

        public void Atualizar (Musica musica)
        {
            context.Update(musica);
            context.SaveChanges();
        }

        public Musica? RecuperarMusica (Musica musica)
        {
            return context.Musicas.FirstOrDefault(m => m.Nome.Equals(musica.Nome));
        }

        public void Remover (Musica musica)
        {
            context.Musicas.Remove(musica);
            context.SaveChanges();
        }
    }
}
