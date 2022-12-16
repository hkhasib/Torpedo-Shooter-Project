import pygame


class PlayerSprite:
    _idle = []
    _hit = []
    _die = []
    _playerRect = None

    def __init__(self):
        self.__player_sprite()

    def __player_sprite(self):
        for i in range(1, 13):
            self._idle.append(pygame.image.load(
                './Assets/Images/Characters/PlayerSprites/Hasib/Idle/skeleton-MovingNIdle_' + str(i) + '.png'))
            self._hit.append(pygame.image.load(
                './Assets/Images/Characters/PlayerSprites/Hasib/GetHit/skeleton-GetHit_' + str(i) + '.png'))
        for k in range(1, 21):
            self._die.append(pygame.image.load(
                './Assets/Images/Characters/PlayerSprites/Hasib/Destroyed/skeleton-Destroy_' + str(k) + '.png'))
        self._playerRect = self._idle[0].get_rect()

        # self.player.set_sprite(idle,hit,die)

    def get_player_sprite(self, pos, sprite_type):
        if sprite_type == "idle":
            return self._idle[pos]
        elif sprite_type == "hit":
            return self._hit[pos]
        elif sprite_type == "die":
            return self._die[pos]

    def get_rect(self):
        return self._playerRect
