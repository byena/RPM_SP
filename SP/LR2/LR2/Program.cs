using System;
// выводим при помощи обертки 
var song = new PlayableTrack("Linkin Park", "In the End", 75);
song.Play();

class Track { // родительский класс
    public string Artist; // поля класса
    public string Title;
    public Track(string artist, string title) { // конструктор класса который при объявлении объедка сохраняет в памяти те параметры которые ему передают
        Artist = artist; // Сохранение
        Title = title;
    }
}

class PlayableTrack : Track { // класс наследуемый от трека, т.е. ребенок
    public int Volume; // новое поле которое пренадлежит только ребенку
    public PlayableTrack(string artist, string title, int volume) : base(artist, title) { // тот же конструктор но уже нового объекта который через : base использует наслудуемые поля
        Volume = volume; // Сохранение
    }
    public void Play() => Console.WriteLine($"Result: {Artist} - {Title} [{Volume}%]"); // просто обертка для вывода этой всё красоты
}