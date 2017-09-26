using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VoiceManager : MonoBehaviour
{
    // TODO: Сделать разнообразие музыки, чтобы не было по одному клипу MonoBehaviour.Invoke AudioClip-length


    // AudioSource для управления воспроизведением Музыки
    AudioSource asMusic;

    // AudioSource для управления воспроизведением звуков кнопок
    AudioSource asUISound;

    // Массив AudioSource для управления воспроизведением всех остальных звуков
    List<AudioSource> asNoiseSound = new List<AudioSource>();
    // Локальная переменная, необходимая для того чтобы собрать все AudioSource на объекте
    AudioSource[] audios;
   


    /// Открытое свойство Одиночки для доступа к полям и методам из других классов
    /**
     * НЕ-Ленивая реализация паттерна Одиночка. Объект нужно предварительно создать на сцене
     * @return ссылка на Одиночку
     */
    public static VoiceManager me { get; private set; }

    /// Корректная инициализации Одиночки
    /**
     * В методе проверяется - существует ли уже экземпляр класса на сцене
     */
    void Awake()
    {
        // Убедимся, что нет других экземпляров этого класса
        if (me != null && me != this)
        {
            // если уже есть, но уничтожаем конфликтную копию
            Messenger.RemoveListener("GameIsStopped", stopAllSound);
            Messenger.RemoveListener("GameIsResumed", startAllSound);
            Destroy(gameObject);

        }
        else
        {
            // Сохраняем ссылку на Одиночку
            me = this;

            //Добавление слушателей на выключение динамиков во время паузы и включение во отжатия паузы
            Messenger.AddListener("GameIsStopped", stopAllSound);
            Messenger.AddListener("GameIsResumed", startAllSound);

            // Объект не будет уничтожаться перед загрузкой следующей сцены
            GameObject.DontDestroyOnLoad(this.gameObject);
        }
    }

    /// Распределение компонентов AudioSource
    /**
     * Динамики обретают имена, инструменты расходятся по своим местам и настраиваются перед 
     * концертом.
     */
    void Start()
    {
        // Кэшируем все компоненты типа AudioSource 
        audios = GetComponents<AudioSource>();

        // 1 динамик - для музыкы
        asMusic = audios[0];

        // 2 - БОНЬКающим кнопкам
        asUISound = audios[1];

        // 3-5 - остальным звукам
        for (int i = 2; i < 5; ++i)
        {
            asNoiseSound.Add(audios[i]);
        }
        // Перемещаем источник звука в центр главной камеры (чтобы не было проблемы с 3Д звуками в 
        // 2Д играх). Если 3Д звуков у вас нет, то можете этого не делать. Без этой строки в iOS звуки
        // почему-то были тише, чем на Android, хотя все звуки были 2D.
        GetComponent<Transform>().position = Camera.main.GetComponent<Transform>().position;

        // NOTE: Здесь используется очень тяжелая операция GetComponent<Type>(). Если собираетесь
        // применять её чаще для одного и того же компонента, то используйте кеширование.
        // Например Transfor Tr = GetComponent<Transform>(); И далее уже пользуетесь Tr.

        // Возпроизведение бекграунд музыки 
        MusicPlayOnStart();

    }

    /// Воспроизвести звук для UI
    /**
     * Метод один раз воспроизводит переданный звук.
     * Рекомендуется использовать только для UI элементов.
     * @param a Звук, который необходимо воспроизвести UI элементу
     */
    public void PlayUI(AudioClip a)
    {
        asUISound.Stop();
        asUISound.clip = a;
        asUISound.Play();
      
     
    }

    /// Запустить воспроизведение музыки
    /**
     * Метод начинает воспроизведение музыки. 
     * После того как молодия заканчивается она играет сначала и так по кругу.
     * Рекомендуется использовать только для музыки.
     * @param a Мелодия для воспроизведения
     */

    public void MusicPlay(AudioClip a)
    {
    /**
        * Проверка, играет ли динамик для бекграунд музыки
        * Если динамик используется(а он 100% используется если запущена музыка со старта) то переопределить клип.
        */
        if (asMusic.isPlaying) {

            // Остановка пред песни
            asMusic.Stop();

            // Назначить новую мелодию
            asMusic.clip = a;

            // Включить воспроизведение в цикле
            asMusic.loop = true;

            // Запустить воспроизведение
            asMusic.Play();
        }

        else {

            // Назначить мелодию
            asMusic.clip = a;

            // Включить воспроизведение в цикле
            asMusic.loop = true;

            // Запустить воспроизведение
            asMusic.Play();

        }
        
    }


    public void MusicPlayOnStart()
    {
        // Назначить мелодию
        asMusic.clip = audios[0].clip;

        // Включить воспроизведение в цикле
        asMusic.loop = true;

        // Запустить воспроизведение
        asMusic.Play();
    }


    /// Выключить музыку
    /**
     * Метод останавливает воспроизведение музыки. 
     */
        public void MusicOFF()
    {
        // Сохраняем в PlayerPrefs - мы же не хотим заставлять игрока выключать музыку 
        // каждый раз при входе в игру 
        PlayerPrefs.SetInt("m", 0);

        // Снижаем громкость до 0
        asMusic.volume = 0.0F;
    }

    /// Включить музыку
    /**
     * Метод продолжает воспроизведение музыки. Операция обратная MusicOFF()
     */
    public void MusicON()
    {
        // Сохраняем в PlayerPrefs факт включения музыки
        PlayerPrefs.SetInt("m", 1);

        // Возвращаем громкость до состояния 1. Громче она быть не может.
        asMusic.volume = 1.0F;
    }


    /// Выключить звуки
    /**
     * Метод выключает все звуки: и звуки UI, и звуки шумов.
     */
    public void SoundOFF()
    {
        // Сохраняем в PlayerPrefs факт о выключении звуков
        PlayerPrefs.SetInt("s", 0);

        // Снижаем громкость динамика, отвечающего за звуки UI элементов, до нуля
        asUISound.volume = 0.0F;

        // Снижаем громкость динамиков, отвечающих за шумы
        for (int i = 0; i < asNoiseSound.Count; ++i)
        {
            asNoiseSound[i].volume = 0.0f;
           
        }
    }

    /// Включить звуки
    /**
     * Метод включает все звуки: и звуки UI, и звуки шумов.
     * Операция, обратная SoundOFF
     */
    public void SoundON()
    {
        PlayerPrefs.SetInt("s", 1);
        asUISound.volume = 1.0F;
        for (int i = 0; i < asNoiseSound.Count; ++i)
        {
            asNoiseSound[i].volume = 1.0f;
            
        }
    }

    /// Остановить музыку
    /**
     * Прекратить играть музыку. Совсем.
     */
    public void MusicStop()
    {
        asMusic.Stop();
    }

    /// Поставить музыку на паузу.
    /**
     * Временно останавливает воспроизведение музыки
     */
    public void MusicPause()
    {
        asMusic.Pause();
    }

    /// Снять музыку с паузы.
    /**
     * Продолжает воспроизведение музыки с места паузы
     */
    public void MusicUnPause()
    {
        asMusic.UnPause();
    }

    //NOTE: Иногда ошибка в одной букве имени метода может стать трагичной.

    /// Закрытый метод для поиска свободно динамика
    /**
     * Ищет не занятый AudioSource из всех "шумовых" в List asNoiseSound
     * @result индекс незанятого динамика
     */
    int TakeFreeAus()
    {
        for (int i = 0; i < asNoiseSound.Count; ++i)
        {
            if (!asNoiseSound[i].isPlaying)
            {
                return i;
            }
        }
        //если свободных нет - добавляем новый динамик
        AudioSource newAS = gameObject.AddComponent <AudioSource>();
        newAS.loop = false;
        newAS.volume = 0.5f;
        newAS.playOnAwake = false;
        asNoiseSound.Add(newAS);

        // возвращаем последний динамик
        return (asNoiseSound.Count - 1);
    }

    /// Метод для воспроизведения шумов
    /**
     * Играет обычный звук, используя закрытый метод TakeFreeAus для поиска свободного динамика
     */
    public void PlayNoiseSound(AudioClip a)
    {
        int freeIndex = TakeFreeAus();
        asNoiseSound[freeIndex].Stop();
        asNoiseSound[freeIndex].clip = a;
        asNoiseSound[freeIndex].Play();
    }


    //Слушатель перенаправляет в данный метод для паузы основных динамиков(шум не включен)
    private void stopAllSound() {
        for (int i = 0; i < audios.Length; i++) {
            if (audios[i].isActiveAndEnabled) {
                audios[i].Pause();
            }
        }
    }

    //Слушатель перенаправляет в данный метод для возобновления проигрывания основных динамиков(шум не включен)
    private void startAllSound()
    {
        for (int i = 0; i < audios.Length; i++)
        {
              audios[i].UnPause();
        }
    }

}
