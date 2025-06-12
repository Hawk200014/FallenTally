using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallenTallyAvalon.Dialogue
{
    public interface IDialog<T> where T : class
    {
        public void Init(T? data = null);

        public void ShowDialogue();

        public T? GetData();

        public void CloseDialogue();
    }
}
