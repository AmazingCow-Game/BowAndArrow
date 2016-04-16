#region Usings
//System
using System;
using System.Collections.Generic;
//XNA
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion //Usings


namespace com.amazingcow.BowAndArrow
{
    public class ResourcesManager
    {
        #region iVars
        ContentManager _contentManager;

        Dictionary<String, Texture2D> _texturesDict;
        #endregion


        #region Singleton
        private static ResourcesManager s_instance;
        public static ResourcesManager Instance
        {
            get
            {
                if(s_instance == null)
                    s_instance = new ResourcesManager();
                return s_instance;
            }
        }
        #endregion //Singleton


        #region CTOR
        private ResourcesManager()
        {
            //Empty...
            _contentManager = GameManager.Instance.Content;
            _texturesDict   = new Dictionary<String, Texture2D>();
        }
        #endregion //CTOR


        #region Public Methods
        public Texture2D GetTexture(String name)
        {
            if(_texturesDict.ContainsKey(name))
                return _texturesDict[name];

            var texture = _contentManager.Load<Texture2D>("sprites/" + name);
            _texturesDict.Add(name, texture);

            return texture;
        }
        #endregion //Public Methods
    }
}

