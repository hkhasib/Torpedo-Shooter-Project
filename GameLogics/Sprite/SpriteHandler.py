from GameLogics.Sprite.PlayerSprite import PlayerSprite
from GameLogics.Sprite.EnemeySprite import EnemySprite


class SpriteHandler:
    def __init__(self):
        self.shark = EnemySprite()
        self.shark.set_idle(0, 9, 'Shark/Shark_move_1_00')

    def get_shark(self, pos, sprite_type):
        return self.shark.get_enemy_sprite(pos, sprite_type)
