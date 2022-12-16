from GameLogics.Character import Character
from GameLogics.Sprite.PlayerSprite import PlayerSprite
import pygame
import math
from pygame import mixer

pygame.init()


class Player(Character):

    def __init__(self, screen, x, y):
        self._sprite = PlayerSprite()
        self._screen = screen
        self._characterX = x
        self._characterY = y
        self.playerSpeed = 0
        self.torpedoState = "ready"
        self.animation_state_switch = True
        self._torpedo_speed = 0
        self._torpedo_x = 0
        self._torpedo_y = 0
        self._bullet_fired = False
        self._bullet_speed = 0
        self._torpedo = pygame.transform.scale(pygame.image.load('./Assets/Images/Objects/09.png'), (60, 30))

        self._t_sound = pygame.mixer.Sound('./Assets/Sounds/Torpedo Launch.wav')

    def place_character(self, animation_speed, status):
        if status == "idle" or status == "hit":
            p_sprite_last = 10.8
        elif status == "die":
            p_sprite_last = 18

        if self._changeNumber > p_sprite_last:
            self._changeNumber = 0

        self._changeNumber += animation_speed
        self._screen.blit(self._sprite.get_player_sprite(math.ceil(self._changeNumber), status),
                          (self._characterX, self._characterY))

    def player_controller(self, event):
        if event.type == pygame.KEYDOWN:
            if event.key == pygame.K_UP:
                self.playerSpeed = 4
            if event.key == pygame.K_DOWN:
                self.playerSpeed = -4
            if event.key == pygame.K_SPACE:
                self._torpedo_x = self._characterX + 150
                self._torpedo_y = self._characterY + 135
                self.torpedoState = "fire"
                self._bullet_speed = 150
                self._t_sound.play()

        elif event.type == pygame.KEYUP:
            if event.key == pygame.K_DOWN or event.key == pygame.K_UP:
                self.playerSpeed = 0
            if event.key == pygame.K_SPACE:
                pygame.mixer.Sound('./Assets/Sounds/Torpedo Launch.wav')

    def player_movement(self):
        self._characterY -= self.playerSpeed
        if self._characterY >= 550:
            self.playerSpeed = 0
        if self._characterY <= -15:
            self.playerSpeed = 0

        if self.torpedoState == "fire":
            self._screen.blit(self._torpedo, (self._torpedo_x, self._torpedo_y))
            self._torpedo_x += self._bullet_speed

            print(self._torpedo_x)
            if self._torpedo_x >= 1189:
                self._bullet_speed = 0
                self._torpedo_x = -300
                self.torpedoState == "ready"

        else:
            self._screen.blit(self._torpedo, (1500, -500))
            self._torpedo_x += 0

    def get_rect(self):
        rect = self._sprite.get_rect()
        rect.x = self._characterX
        rect.y = self._characterY
        return rect

    def shoot(self, x, y):
        self._screen.blit(self._torpedo, (x, y))
        self._bullet_fired = True
