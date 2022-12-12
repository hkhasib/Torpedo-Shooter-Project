class Parallax:

    __scroll = 0

    def __init__(self, bg_images, limit, screen):
        self.__bg_images = bg_images
        self.__bg_width = self.__bg_images[0].get_width()
        self.__limit = limit
        self.__screen = screen

    def drawParallax(self):
        for x in range(self.__limit):
            speed = 1
            for i in self.__bg_images:
                self.__screen.blit(i, ((x * self.__bg_width) - self.__scroll * speed, 0))
                speed += 0.2

    def setScroll(self, value):
        self.__scroll = value

    def getScroll(self):
        return self.__scroll
