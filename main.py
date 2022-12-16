import pygame
from GameLogics.Parallax import Parallax
import math
import random
import time
from GameLogics.Player import Player
from GameLogics.Enemy import Enemy
from GameLogics.Sprite.SpriteHandler import SpriteHandler
from pygame import mixer
pygame.init()
clock = pygame.time.Clock()

# Game variables
FPS = 75
SCREEN_WIDTH = 1280
SCREEN_HEIGHT = 720
Running = True
life = 3
startTime = 0
# Screen and Game Identity
screen = pygame.display.set_mode((SCREEN_WIDTH, SCREEN_HEIGHT))
pygame.display.set_caption("Endless Scroll")

# Background
layer = pygame.image.load('./Assets/Images/Backgrounds/Level1/layer.png').convert_alpha();
scroll = 0
bg_images = []
for i in range(1, 6):
    bg_image = pygame.image.load('./Assets/Images/Backgrounds/Level1/' + str(i) + '.png').convert_alpha()
    bg_images.append(bg_image)

parallax = Parallax(bg_images, 22, screen)

# Background sound

mixer.music.load('./Assets/Sounds/UnderwaterAmbience.wav')
mixer.music.play(-1)

sharkCollission=mixer.Sound('./Assets/Sounds/Game Marker Hit 5.wav')

sharkCollissionSound=False


# Player
playerX = 0
playerY = 300

playerChange = 0
p_sprite_last = 2

player = Player(screen, playerX, playerY)
player_status = "idle"

enemyX = 1100
enemyY = 200
enemy = Enemy(screen, 1, enemyX, enemyY)
enemy_change = 0.9

all_sprites = SpriteHandler()
while Running:
    clock.tick(FPS)
    parallax.drawParallax()
    # player(math.ceil(playerChange))

    if player_status == "idle" or player_status == "hit":
        p_sprite_last = 10.8
    elif player_status == "die":
        p_sprite_last = 18

    if playerChange > p_sprite_last:
        playerChange = 0

    player.player_movement()

    enemy_change += 0.1

    if enemy_change > 8:
        enemy_change = 0

    player.place_character(0.3, player_status)

    enemy.place_character(all_sprites.get_shark(math.ceil(enemy_change), 'idle'))
    # pygame.draw.rect(screen, (255, 255, 255), player.get_rect(), 2)
    # pygame.draw.rect(screen, (255, 255, 255), enemy.get_rect(), 2)
    enemyX -= 2
    if enemy.get_x() <= -250:
        enemyX = 1300
        enemyY = random.randrange(0, 600)
    enemy.set_x(enemyX)
    enemy.set_y(enemyY)

    if player.get_rect().colliderect(enemy.get_rect()):
        if player_status != "hit" or player_status != "die":
            if not sharkCollissionSound:
                sharkCollission.play()
                sharkCollissionSound=True
            startTime = time.time()
            player_status = "hit"

        # if abs(startTime - time.time()) >= 3:
        #     enemyX = 4050
    else:
        player_status = "idle"
        sharkCollissionSound = False

    if scroll < 10000:
        scroll += 0.5
    parallax.setScroll(scroll)

    for event in pygame.event.get():
        if event.type == pygame.QUIT:
            Running = False
        player.player_controller(event)

    screen.blit(layer, (0, 0))
    pygame.display.update()
pygame.quit()
