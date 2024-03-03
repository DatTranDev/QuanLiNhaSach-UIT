using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiNhaSach.ViewModel.MessageBoxVM
{
    internal class MessageBoxViewModel 
    {
        private string text;

        public string Text
        {
            get { return text; }
            set { text = value; }
        }
        public MessageBoxViewModel(string _text)
        {
            text = _text;
        }
    }
}
