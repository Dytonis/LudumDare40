using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LD40.UI.Menus
{
    /// <summary>
    /// EndScreenMenu inherits Menu, so we can assume it can be used with MenuManager. 
    /// </summary>
    public class EndScreenMenu : Menu
    {
        public TextDouble Title;
        public TextDouble TimeText;
        public TextDouble ShotText;
        public TextDouble LetterGrade;

        public Transform GoodEmote;
        public Transform BadEmote;
    }
}
