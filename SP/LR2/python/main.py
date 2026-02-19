class Track:
    # родительский метод для записи базовых данных
    def set_base_info(self, artist, title):
        # убираем лишние пробелы и приводим к формату заголовка
        self.artist = artist.strip().title()
        self.title = title.strip().capitalize()

class PlayableTrack(Track):
    # подкласс родительского метода (ребенок)
    def set_full_info(self, artist, title, volume, duration_sec):
        # используем метод родителя дополняя его новым полем(атрибутом)
        self.set_base_info(artist, title)
        # ограничиваем громкость в пределах от 0 до 100
        self.volume = max(0, min(volume, 100))
        self.duration_sec = duration_sec

    # вычисляем длительность в формате ММ:СС
    def get_time_format(self):
        mins = self.duration_sec // 60
        secs = self.duration_sec % 60
        return f"{mins}:{secs:02d}"

    # метод для изменения громкости (манипуляция с состоянием)
    def adjust_volume(self, amount):
        self.volume = max(0, min(self.volume + amount, 100))
        print(f"Громкость изменена на {self.volume}%")

    def play(self):
        time = self.get_time_format()
        print(f"Result: {self.artist} - {self.title} [{time}] | Vol: {self.volume}%")

# создаем объект а потом передаем в него параметры
song = PlayableTrack()
# передаем данные с ошибками в регистре и завышенной громкостью
song.set_full_info("  lInkin pArk  ", "in the END", 120, 216)

song.play()
# уменьшаем громкость
song.adjust_volume(-30)
song.play()