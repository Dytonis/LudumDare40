using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        public void Retry()
        {
            Game.GetFlasher().FlashWin();
            Game.GetController().StartReset();
            Game.GetAudio().Flash.Play();
            Destroy(gameObject);
        }

        public void Next()
        {
            SceneManager.LoadScene(Game.NextLevelTitle, LoadSceneMode.Single);
            Game.LevelCompleted = false;
            Time.timeScale = 1;
        }
    }
}
