from abc import ABC

from GameLogics.Character import Character
import pygame


class Enemy(Character):

    def __init__(self, screen, speed, posX, posY):
        self._sprites = []
        self._speed = speed
        self._characterX = posX
        self._characterY = posY
        self._screen = screen
        self._rect_of_enemy=None
        # for i in range(0, 9):
        #     self._sprites.append(pygame.image.load(
        #         './Assets/Images/Characters/EnemySprites/Shark/Shark_move_1_00' + str(i) + '.png'))

    def enemy_movement(self, xStart, xEnd):
        if self._characterX <= xEnd:
            self._characterX = xStart

    def place_character(self, sprite):
        self._screen.blit(sprite,(self._characterX, self._characterY))
        self._rect_of_enemy=sprite.get_rect()

    def get_rect(self):
        rect = self._rect_of_enemy
        rect.x = self._characterX
        rect.y = self._characterY
        return rect

