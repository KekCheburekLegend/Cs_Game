using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameBattle.models;
using Microsoft.Xna.Framework;

namespace GameBattle.view
{
    internal static class View
    {
        public static Rectangle GetSourceRectangle(this Player player)
        {
            return new Rectangle(
                player.CurrentFrameX * (player.Width + 105),
                player.CurrentFrameY * player.Height,
                player.Width,
                player.Height
            );
        }



        public static void UpdateAnimation(this Player player, float deltaTime)
        {
            if (player.IsAttacking) 
            {
                player.FrameTimer += deltaTime;
                if (player.FrameTimer >= 0.1f) 
                {
                    player.FrameTimer = 0f;
                    player.CurrentFrameX++;
                    player.CurrentFrameY = 3; 
                    if (player.CurrentFrameX >= 4) 
                        player.CurrentFrameX = 1;
                }
                return;
            }
            if (player.IsMoving)
            {
                player.FrameTimer += deltaTime;
                if (player.FrameTimer >= player.FrameInterval)
                {
                    player.FrameTimer = 0f;
                    player.CurrentFrameX++;
                    player.CurrentFrameY = 2;
                    if (player.CurrentFrameX >= player.TotalFramesX)
                        player.CurrentFrameX = 1;
                }
            }
            else
            {
                player.CurrentFrameX = 1;
                player.CurrentFrameY = 0;
                player.FrameTimer = 0f;
            }
        }
    }
}
