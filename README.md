### 2048 steps

[ODEV ICIN BURAYA BAK](#odev)

After making the [Sudoku game](https://github.com/sayawan/sudoku), this time we decided to make a simple clone of 2048 game. So there are step by step explanation of each function as it was designed in functional way.

Firstly, it somehow harder to get the point of the game. So lets look up what is going on here. We should check **Text** property of every TextBox on every key press. As shown in the diagram below, our loop logic is:

**Up:**
```
[1]-[5]-[9]-[13]
    [5]-[9]-[13]
        [9]-[13]
```

**Down:**
```
[13]-[9]-[5]-[1]
     [9]-[5]-[1]
         [5]-[1]
```

**Left:**

```
[1]-[2]-[3]-[4]
    [2]-[3]-[4]
        [3]-[4]
```

**Right:**
```
[4]-[3]-[2]-[1]
    [3]-[2]-[1]
        [2]-[1]
```

It is clear that we use loop inside loop. One is **x** another **x-1** so we can compare to fields content. Then there some if - else statement.

[more documentation coming]

----------

ODEV
-------------------

> **NESNE TABANLI ODEV:**
(siyahlar yapildi)

> - **3x3, 4x4, 5x5, 6x6, 7x7, boyutundaki kutular ile 2048 oynanabilecektir.**
> - **Oyun ilk olarak başladığında 2 tane 2 sayısı ekranda rastgele kutularda çıkacaktır.**
> - **Sadece aynı sayılar toplanarak oyun devam edecektir (2+2, 4+4, 8+8, 16+16 ...)**
> - **Oyun, klavyenin yön tuşları ile oynanacaktır.**
**a. Sağa hareket**
**b. Sola hareket**
**c. Aşağı hareket**
**d. Yukarı hareket**
> - Her hareket sonrasında 1 tane %90 olasılıklı 2 veya %10 olasılıklı 4 sayısı ekranda rastgele bir kutuda çıkacaktır.
> - Oyunda hareket şansı kalmadı ise (tüm mkutular dolu ise oyunun tamamlandığı ile ilgili bir mesaj verilecek.
> - **Her sayı için farklı renk kullanılacaktır.**
> - **Oyun fonksiyonel olarak tasarlanacaktır.**

----------