import pygame
from parallax import Parallax
import math

pygame.init()
clock = pygame.time.Clock()
FPS = 60
SCREEN_WIDTH = 1280
SCREEN_HEIGHT = 720
screen = pygame.display.set_mode((SCREEN_WIDTH, SCREEN_HEIGHT))
pygame.display.set_caption("Endless Scroll")

scroll = 0
bg_images = []
for i in range(1, 6):
    bg_image = pygame.image.load('./Layers/' + str(i) + '.png').convert_alpha()
    bg_images.append(bg_image)

parallax = Parallax(bg_images, 22, screen)

Running = True
while Running:
    while Running:
        clock.tick(FPS)

        parallax.drawParallax()

        if scroll < 10000:
            scroll += 1
        parallax.setScroll(scroll)
        for event in pygame.event.get():
            if event.type == pygame.QUIT:
                Running = False
        pygame.display.update()
pygame.quit()
