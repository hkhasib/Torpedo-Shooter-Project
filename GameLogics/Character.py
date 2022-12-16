from abc import ABC, abstractmethod
import pygame


class Character:
    _sprites = []
    _hit_sprites = []
    _die_sprites = []
    _Speed = 0
    _sprite_index = 0
    _characterX = 0
    _characterY = 0
    _screen = None
    _changeNumber = 0

    _spriteLast = 0

    def __int__(self):
        pygame.init()
        pass

    def place_character(self, spritePos):
        self._screen.blit(self._sprites[spritePos], (self._characterX, self._characterY))

    def get_sprite_len(self):
        return len(self._sprites)

    def set_x(self, x):
        self._characterX = x

    def set_y(self, y):
        self._characterY = y

    def get_x(self):
        return self._characterX

    def get_y(self):
        return self._characterY
