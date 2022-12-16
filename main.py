import pygame, sys
from parallax import Parallax
from button import Button
import math

pygame.init()
clock = pygame.time.Clock()
FPS = 60
SCREEN_WIDTH = 1280
SCREEN_HEIGHT = 720
screen = pygame.display.set_mode((SCREEN_WIDTH, SCREEN_HEIGHT))

def get_font(size): # Returns Press-Start-2P in the desired size
    return pygame.font.Font("Assets/font.ttf", size)

def menu():
    while True:
        pygame.display.set_caption("Menu")
        bg_image = pygame.image.load('Assets/Background.png').convert_alpha()
        screen.blit(bg_image,(0,0))
        MENU_MOUSE_POS = pygame.mouse.get_pos()

        MENU_TEXT = get_font(100).render("MAIN MENU", True, "#b68f40")
        MENU_RECT = MENU_TEXT.get_rect(center=(640, 100))

        screen.blit(MENU_TEXT, MENU_RECT)
        PLAY_BUTTON = Button(image=pygame.image.load("assets/Play Rect.png"), pos=(640, 250), 
            text_input="PLAY", font=get_font(75), base_color="#d7fcd4", hovering_color="White")
#   OPTION FOR OTHER BUTTON TOO 
        for button in [PLAY_BUTTON]:
            button.changeColor(MENU_MOUSE_POS)
            button.update(screen)
        for event in pygame.event.get():
            if event.type == pygame.QUIT:
                pygame.quit()
                sys.exit()
            if event.type == pygame.MOUSEBUTTONDOWN:
                if PLAY_BUTTON.checkForInput(MENU_MOUSE_POS):
                    play()
        pygame.display.update()



def play():
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

menu()
