class Track:
    # родительский метод для записи базовых данных
    def set_base_info(self, artist, title):
        self.artist = artist
        self.title = title

class PlayableTrack(Track):
    # подкласс родительского метода (ребенок)
    def set_full_info(self, artist, title, volume):
        # используем метод родителя дополняя его новым полем(атрибутом)
        self.set_base_info(artist, title)
        self.volume = volume
    def play(self):
        print(f"Result: {self.artist} - {self.title} [{self.volume}%]")
# создаем объект а потом передаем в него параметры
song = PlayableTrack()
song.set_full_info("Linkin Park", "In the End", 75)
song.play()