import pygame


class EnemySprite:
    _idle = []
    _hit = []
    _die = []

    def __init__(self):
        pass

    def set_idle(self, start, end, asset_string):
        for i in range(start, end):
            self._idle.append(pygame.image.load(
                './Assets/Images/Characters/EnemySprites/' + asset_string + str(i) + '.png'))

    def set_die(self, start, end, asset_string):
        for i in range(start, end):
            self._die.append(pygame.image.load(
                './Assets/Images/Characters/EnemySprites/' + asset_string + str(i) + '.png'))

    def set_hit(self, start, end, asset_string):
        for i in range(start, end):
            self._hit.append(pygame.image.load(
                './Assets/Images/Characters/EnemySprites/' + asset_string + str(i) + '.png'))

    def get_enemy_sprite(self, pos, sprite_type):
        if sprite_type == "idle":
            return self._idle[pos]
        elif sprite_type == "hit":
            return self._hit[pos]
        elif sprite_type == "die":
            return self._die[pos]

    def sprite_len(self, sprite_type):
        if sprite_type == "idle":
            return len(self._idle)
        elif sprite_type == "hit":
            return len(self._hit)
        elif sprite_type == "die":
            return len(self._die)

    def get_rect(self):
        return self._idle[0].get_rect()
