using System;

// выводим при помощи обертки 
// передаем данные с лишними пробелами и завышенной громкостью для проверки
var song = new PlayableTrack("  Linkin Park  ", "In the End", 150, 216);
song.Play();

song.AdjustVolume(-30);
song.Play();

class Track { // родительский класс
    public string Artist; // поля класса
    public string Title;
    public Track(string artist, string title) { // конструктор класса который при объявлении объедка сохраняет в памяти те параметры которые ему передают
        Artist = artist.Trim(); // Сохранение+убираем лишнее
        Title = title.Trim();
    }
}

class PlayableTrack : Track { // класс наследуемый от трека, т.е. ребенок
    public int Volume; // новое поле которое пренадлежит только ребенку
    public int DurationSeconds; // поле для хранения длительности в секундах

    public PlayableTrack(string artist, string title, int volume, int duration) : base(artist, title) { // тот же конструктор но уже нового объекта который через : base использует наслудуемые поля
        Volume = Math.Clamp(volume, 0, 100); // Сохранение
        DurationSeconds = duration;
    }

    //превращаем секунды в формат 0:00 при помощи форматиров
    public string GetFormattedTime() {
        int minutes = DurationSeconds / 60;
        int seconds = DurationSeconds % 60;
        return $"{minutes}:{seconds:D2}";
    }

    // нормируем громкость при помощи math.clamp
    public void AdjustVolume(int change) {
        Volume = Math.Clamp(Volume + change, 0, 100);
        Console.WriteLine($"система: громкость изменена на {Volume}%");
    }

    public void Play() => Console.WriteLine($"Result: {Artist} - {Title} [{GetFormattedTime()}] | Vol: {Volume}%"); // просто обертка для вывода этой всё красоты
}