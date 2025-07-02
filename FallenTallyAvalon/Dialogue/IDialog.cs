using Avalonia.Controls;
using FallenTally.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallenTally.Dialogue
{
    public interface IDialog<T> where T : class
    {
        public void Init(T? data = null);

        public void ShowDialogue();

        public T? GetData();

        public void CloseDialogue();

        public Task<T?> ShowDialogueAsync(Window owner);
    }
}
