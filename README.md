# tetris
bu wdf projemizde tetris oyununu yapıcaz
<br>
c# kodlarıyla birlikte oyunun temel dinamiklerini oluşturcaz
<br>
tetris oyununu açıklamak gerekirse, belirli bir çerçeve içinde yukardan tuşlarla yönetebildiğimiz bloklar gelir ve bu bloklarda zeminde boşluk bırakmadan doldurduğumuzda skor elde edebildiğimiz bir oyundur.
<br>
Kodların içindeki sınıflar,özelikleri,pencereler gibi bazı işlevlerin sözel özeti:

#ÖZET
1. `board` Sınıfı:
   - `Rows`, `Cols`, `Score`, `LinesFilled`, `currTetramina` ve `BlockControls` gibi özel değişkenlere sahiptir.
   - `TetrisGrid` parametresi alarak bir oyun tahtası oluşturur.
   - `getScore()` ve `getLines()` metotları, puanı ve dolu satır sayısını döndürür.
   - `currTetraminaDraw()`, mevcut tetraminayı tahtaya çizer.
   - `currTetraminaErase()`, mevcut tetraminayı tahtadan siler.
   - `CheckRows()`, dolu satırları kontrol eder ve gerektiğinde siler.
   - `RemoveRow(int row)`, belirli bir satırı siler.
   - `CurrtetraminamoveLeft()`, mevcut tetraminayı sola hareket ettirir.
   - `CurrtetraminamoveRight()`, mevcut tetraminayı sağa hareket ettirir.
   - `CurrtetraminamoveDown()`, mevcut tetraminayı aşağı hareket ettirir ve dolu satırları kontrol eder.
   - `CurrtetraminamoveRotate()`, mevcut tetraminayı döndürür.

2. `Tetramina` Sınıfı:
   - `currPosition`, `currShape`, `currColor` ve `rotate` gibi özel değişkenlere sahiptir.
   - `getcurrColor()`, `getCurrPosition()` ve `getCurrShape()` metotları, sırasıyla mevcut rengi, pozisyonu ve şekli döndürür.
   - `movleft()`, `movright()`, `movdown()` ve `movrotate()`, tetraminayı sırasıyla sola, sağa, aşağıya hareket ettirir ve döndürür.
   - `SetRandomShape()`, rastgele bir şekil oluşturur ve onu döndürür.

3. `MainWindow` Sınıfı:
   - Tetris oyununun ana penceresini temsil eder.
   - `Timer` ve `myBoard` gibi özel değişkenlere sahiptir.
   - `GameStart()`, oyuna başlamak için tahtayı temizler ve yeni bir oyun tahtası oluşturur.
   - `GameTick()`, oyun süresi ilerledikçe çağrılan bir olay işleyicisidir. Tahtadaki skoru ve dolu satır sayısını günceller ve mevcut tetraminayı aşağı hareket ettirir.
   - `GamePause()`, oyunu duraklatır veya devam ettirir.
   - `HandleKeyDown()`, klavye tuşlarına basıldığında çağrılan bir olay işleyicisidir. Tuşlara göre tetraminayı hareket

#tasarım bölümü
projemizi wdf platformunda yapcaz visual stiduo da bulması biraz "wdf uygulaması",etiketinde ise `"c#","windows","masaüstü"` olacaktır.
doğru yerde projemizi oluşturduğumuzan emin olduktan sonra ,Mainwindow.Xaml penceresine geliyoruz burası işin tasarım kısmı tasarımı yapıcaz öncelikle,
`xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"`
`xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"`
yukarda verdiğim iki kaynağı ekliyoruz,Title satırında pencernin ismini istediğinizi verebilirsiniz pencerenin üstünde gözükcek ve yükselik ve genişliği ayarlıyoruz.
570 e 700 olarak ayarladım bende.
`KeyDown ="HandleKeyDown"`yazıyoruz bu kod klavyeden tuş basıldığında algılanmasına istenilen fonksiyonun çağrılmasını sağlıycak.
`'Initialized = "MainWindow_Initilized" Background ="DimGray">' `kod satırında
`MainWindow_Initialized` adlı bir metot, Initialized olayını işleyen bir metottur. Bu metot, pencerenin başarıyla başlatılması durumunda otomatik olarak çağrılır ve gerekli işlemleri gerçekleştirebiliriz.yanlız bu yazdıklarımız window içince olamlı '<window .....>'bu şekilde yani .
şimdi `<DockPanel LastChildFill="True"></DockPanel>`satrını oluşturalım dockpanel içine yerleştirilen elemanların yatay ve dikey hizlanmasını sağlar `lastchildfill` ise
son yerleşen elemanın boşluğun tamamnı doldurcak kadar büyütür.

`<StackPanel DockPanel.Dock="Right" Width="150"></StackPanel>`bu kodda  stackpanel elemanları dikey olarak yerleştirir ve sağ sabitler genişliği 150 olarak ayarladık.
stack panelin içinede `<Label Content="Label" Height="56" x:Name="Scores" FontSize="28" FontWeight="Bold"  />` Scores ve Lines adında sadece name farklı olcak şekilde 2 satır oluşturalım yazı oluşturduk kısaca.
`<Grid Name="MainGrid" Height="500" Width="250"></Grid>` grid ızgara demek kolon ve satır oluşturmak tablo yapmak için kullanılır.
`<Grid.RowDefinitions></Grid.RowDefinitions>` satır oluşturmak için bu kalıbı kullanıyoruz içine `<RowDefinition/>`tan 22 tane ekliyoruz 22 tane satır oluşucak.
`<Grid.ColumnDefinitions></Grid.ColumnDefinitions>` aynı şekilde kolon oluşturmak içinde aynı şeyler geçerli `<ColumnDefinition/>`da 10 kolon ekliyoruz tetris bloklarının gelceği kareli düzlem hazır.Artık tasarım kısmı bitti Mainwindow.xaml.cs penceresine gecebiliriz.
   
 
Girdiğimizde hazır gelen c# kütüphaneler yeterli olcaktır aynen kalabilir.`namespace`in içine public olacak şekilde board sınıfı oluşturuyoruz.
'Rows','cols','scores', gibi bazı gerekli değişkenleri tanımlıyoruz yeri geldikce ekleme yapın yeni değişkenler ekleyin.
```
   public board(Grid TetrisGrid)
        {
            Rows = TetrisGrid.RowDefinitions.Count;
            Cols = TetrisGrid.ColumnDefinitions.Count;
            Score = 0;
            BlockControls = new System.Windows.Controls.Label[Cols, Rows];
            for (int i = 0; i < Cols; i++)
            {
                for (int j = 0; j < Rows; j++)
                {
                    BlockControls[i, j] = new System.Windows.Controls.Label();
                    BlockControls[i, j].Background = NoBrush;
                    BlockControls[i, j].BorderBrush = SilverBrush;
                    BlockControls[i, j].BorderThickness = new Thickness(1, 1, 1, 1);
                    Grid.SetRow(BlockControls[i, j], j);
                    Grid.SetColumn(BlockControls[i, j], i);
                    TetrisGrid.Children.Add(BlockControls[i, j]);
                }
            }
            currTetramina = new Tetramina();
            currTetraminaDraw();
        }
```
   Bu kod bloğu, `board` sınıfının yapıcı (constructor) metodu olarak tanımlanmıştır. Bu metot, `TetrisGrid` adında bir `Grid` kontrolü alır ve tetris oyun tahtasını oluşturmak için kullanılır.

İlk olarak, `Rows` ve `Cols` değişkenleri `TetrisGrid` kontrolünün `RowDefinitions` ve `ColumnDefinitions` koleksiyonlarının sayılarını alarak belirlenir. Bu sayılar, tetris tahtasının satır ve sütun sayılarını temsil eder.

Sonra, `Score` değişkeni sıfırlanır. `BlockControls` adında bir `Label` dizisi oluşturulur. Bu dizi, tetris tahtasının her bir bloğunu temsil eder. Dizinin boyutu `Cols` ve `Rows` değerlerine göre belirlenir.

Daha sonra, iki döngü kullanılarak `BlockControls` dizisinin her bir elemanı için `Label` nesneleri oluşturulur. Her bir `Label` nesnesi için arka plan (`Background`) özelliği `NoBrush` değeri ile ayarlanır. `BorderBrush` özelliği `SilverBrush` değeriyle, `BorderThickness` özelliği ise 1 birim kalınlığında bir kenarlık olacak şekilde ayarlanır. Ardından, `Grid.SetRow` ve `Grid.SetColumn` metotlarıyla her bir `Label` nesnesi için doğru satır ve sütun numarası belirlenir ve ilgili `Label` nesnesi `TetrisGrid.Children` koleksiyonuna eklenir. Böylece, her bir blok, `TetrisGrid` kontrolü içindeki doğru konuma yerleştirilmiş olur.

Döngüler tamamlandıktan sonra, `currTetramina` adında yeni bir `Tetramina` nesnesi oluşturulur ve `currTetraminaDraw` metodu çağrılarak bu tetraminanın çizimi yapılır. Bu işlem, oyun tahtasında başlangıçta aktif olan tetraminanın görüntülenmesini sağlar.
```
      private void currTetraminaDraw()
        {
            Point position = currTetramina.getCurrPosition();
            Point[] Shape = currTetramina.getCurrShape();
            Brush Color = currTetramina.getcurrColor();
            foreach (Point S in Shape)
            {
                BlockControls[(int)(S.X + position.X) + ((Cols / 2) - 1),
                    (int)(S.Y + position.Y) + 2].Background = Color;
            }
        } 
```
   Bu metodun amacı, `currTetraminaDraw` adında bir özel metot olarak tanımlanmıştır. Bu metot, `currTetramina` adlı örneğin mevcut pozisyonunu (`position`), şeklini (`Shape`) ve rengini (`Color`) alarak oyun tahtasında ilgili konumlara blokları çizer.

Metot, `currTetramina` örneğinden mevcut pozisyonu (`position`), şekli (`Shape`) ve rengi (`Color`) alır. Ardından, `Shape` dizisindeki her bir nokta (`S`) için aşağıdaki işlemleri gerçekleştirir:

1. Noktanın X değeriyle pozisyonun X değeri toplanır ve `(Cols / 2) - 1` eklenir.
2. Noktanın Y değeriyle pozisyonun Y değeri toplanır ve 2 eklenir.
3. Elde edilen konum, `BlockControls` dizisindeki ilgili hücrenin arkaplan rengini (`Color`) alır.

Bu işlemler sonucunda, `currTetramina` örneğinin şekline göre oyun tahtasında bloklar çizilir ve ilgili konumlarında belirtilen arkaplan renkleri atanır.

```
 private void currTetraminaErase()
        {
            Point position = currTetramina.getCurrPosition();
            Point[] Shape = currTetramina.getCurrShape();
            foreach (Point S in Shape)
            {
                BlockControls[(int)(S.X + position.X) + ((Cols / 2) - 1),
                    (int)(S.Y + position.Y) + 2].Background = NoBrush;
            }
        }
```   
   Bu metodun amacı, `currTetraminaErase` adında bir özel metot olarak tanımlanmıştır. Bu metot, `currTetramina` adlı örneğin mevcut pozisyonunu (`position`) ve şeklini (`Shape`) alarak oyun tahtasındaki ilgili blokları siler.

Metot, `currTetramina` örneğinden mevcut pozisyonu (`position`) ve şekli (`Shape`) alır. Ardından, `Shape` dizisindeki her bir nokta (`S`) için aşağıdaki işlemleri gerçekleştirir:

1. Noktanın X değeriyle pozisyonun X değeri toplanır ve `(Cols / 2) - 1` eklenir.
2. Noktanın Y değeriyle pozisyonun Y değeri toplanır ve 2 eklenir.
3. Elde edilen konum, `BlockControls` dizisindeki ilgili hücrenin arkaplan rengi `NoBrush` ile değiştirilir.

Bu işlemler sonucunda, `currTetramina` örneğinin şekline göre oyun tahtasında bulunan blokların arkaplan renkleri `NoBrush` ile değiştirilir, yani bloklar silinir.

```
   
   private void CheckRows()
        {
            bool full;
            for (int i = Rows - 1; i > 0; i--)
            {
                full = true;
                for (int j = 0; j < Cols; j++)
                {
                    if (BlockControls[j, i].Background == NoBrush)
                    {
                        full = false;
                    }
                }
                if (full)
                {
                    RemoveRow(i);
                    Score += 100;
                    LinesFilled += 1;
                }
            }
        }
```
                                         Bu metodun amacı, `CheckRows` adında bir özel metot olarak tanımlanmıştır. Bu metot, oyun tahtasındaki satırları kontrol eder ve dolu olan satırları kaldırır.

Metot, bir `for` döngüsü kullanarak yukarıdan aşağıya doğru satırları kontrol eder. İlk satır (indeks 0) kontrol edilmez, çünkü oyun tahtasının en üstünde yer alan bir satır olduğu düşünülür.

Her bir satır için, `full` adında bir boolean değişken tanımlanır ve başlangıçta `true` olarak ayarlanır. Daha sonra, iç içe bir `for` döngüsü kullanılarak satırdaki her bir sütun kontrol edilir. Eğer herhangi bir sütunun arkaplan rengi `NoBrush` ise (`BlockControls[j, i].Background == NoBrush`), `full` değişkeni `false` olarak güncellenir.

Eğer `full` değişkeni hala `true` ise, bu demektir ki o satır doludur. Dolu satırın kaldırılması için `RemoveRow` metodu çağrılır. Ardından, skora 100 puan eklenir (`Score += 100`) ve dolu satır sayısı bir arttırılır (`LinesFilled += 1`).

```
  private void RemoveRow(int row)
        {
            for (int i = row; i > 2; i--)
            {
                for (int j = 0; j < Cols; j++)
                {
                    BlockControls[j, i].Background = BlockControls[j, i - 1].Background;
                }
            }
        }                                     
```    Bu `RemoveRow` adlı özel metot, belirli bir satırın kaldırılmasını sağlar. Parametre olarak kaldırılacak satırın indeksi (`row`) alınır.
Metot, bir `for` döngüsü kullanarak `row` indeksinden başlayarak 2 ye kadar olan (dahil olmayan) satırları aşağıya doğru iter. Her bir satır için, bir iç içe `for` döngüsü kullanılarak sütunlar kontrol edilir.
Her bir sütun için, `BlockControls[j, i].Background` değeri, bir üst satırdaki (`i - 1`) aynı sütundaki arkaplan rengine eşitlenir. Böylece, bir üst satırdaki blokların rengi, altındaki boşluğa yerleştirilmiş gibi görünür.
                                         
``` 
   public void CurrtetraminamoveLeft()
        {
            Point Position = currTetramina.getCurrPosition();
            Point[] Shape = currTetramina.getCurrShape();
            bool move = true;
            currTetraminaErase();
            foreach (Point S in Shape)
            {
                if (((int)(S.X + Position.X) + ((Cols / 2) - 1) - 1) < 0)
                {
                    move = false;
                }
                else if (BlockControls[((int)(S.X + Position.X) + ((Cols / 2) - 1) - 1),
                    (int)(S.Y + Position.Y) + 2].Background != NoBrush)
                {
                    move = false;
                }
            }
            if (move)
            {
                currTetramina.movleft();
                currTetraminaDraw();
            }
            else
            {
                currTetraminaDraw();
            }
        }                                      
``` 
   Bu `CurrtetraminamoveLeft` adlı yöntem, mevcut tetraminanın sola hareketini gerçekleştirir. İşlem sırasında mevcut tetraminanın pozisyonu (`Position`) ve şekli (`Shape`) alınır. İlk olarak, hareketin mümkün olup olmadığını kontrol etmek için bir dizi koşul denetlenir.

Önce, her bir şeklin sol tarafındaki blokların tahtanın sol kenarına çarpmayacağını kontrol ederiz. Bunun için, `((int)(S.X + Position.X) + ((Cols / 2) - 1) - 1) < 0` koşulunu kontrol ederiz. Eğer bu koşul sağlanıyorsa (`true`), hareketi engellemek için `move` değişkenini `false` olarak ayarlarız.

Daha sonra, her bir şeklin sol tarafındaki blokların solunda başka bir blok olup olmadığını kontrol ederiz. Bunun için, `BlockControls[((int)(S.X + Position.X) + ((Cols / 2) - 1) - 1), (int)(S.Y + Position.Y) + 2].Background != NoBrush` koşulunu kontrol ederiz. Eğer bu koşul sağlanıyorsa (`true`), hareketi engellemek için `move` değişkenini `false` olarak ayarlarız.

Koşulların değerlendirilmesinden sonra, `move` değişkeni kontrol edilir. Eğer `move` `true` ise, tetramina sola hareket ettirilir (`currTetramina.movleft()`) ve `currTetraminaDraw()` çağrısı ile güncellenen tetramina tahtaya çizilir. Eğer `move` `false` ise, tetramina hareket etmez ve yalnızca `currTetraminaDraw()` çağrısıyla tetramina tahtaya çizilir.benzer şeyler sola kaydırma içinde geçerli

```
public void CurrtetraminamoveDown()
        {
            Point Position = currTetramina.getCurrPosition();
            Point[] Shape = currTetramina.getCurrShape();
            bool move = true;
            currTetraminaErase();
            foreach (Point S in Shape)
            {
                if (((int)(S.Y + Position.Y) + 2 + 1) >= Rows)
                {
                    move = false;
                }
                else if (BlockControls[((int)(S.X + Position.X) + ((Cols / 2) - 1)),
                    (int)(S.Y + Position.Y) + 2 + 1].Background != NoBrush)
                {
                    move = false;
                }
            }
            if (move)
            {
                currTetramina.movdown();
                currTetraminaDraw();
            }
            else
            {
                currTetraminaDraw();
                CheckRows();
                currTetramina = new Tetramina();
            }
        }
```
   Bu `CurrtetraminamoveDown` adlı yöntem, mevcut tetraminanın aşağı doğru hareketini gerçekleştirir. İşlem sırasında mevcut tetraminanın pozisyonu (`Position`) ve şekli (`Shape`) alınır. İlk olarak, hareketin mümkün olup olmadığını kontrol etmek için bir dizi koşul denetlenir.

Önce, her bir şeklin alt tarafındaki blokların tahtanın alt kenarına çarpmayacağını kontrol ederiz. Bunun için, `((int)(S.Y + Position.Y) + 2 + 1) >= Rows` koşulunu kontrol ederiz. Eğer bu koşul sağlanıyorsa (`true`), hareketi engellemek için `move` değişkenini `false` olarak ayarlarız.

Daha sonra, her bir şeklin alt tarafındaki blokların altında başka bir blok olup olmadığını kontrol ederiz. Bunun için, `BlockControls[((int)(S.X + Position.X) + ((Cols / 2) - 1)), (int)(S.Y + Position.Y) + 2 + 1].Background != NoBrush` koşulunu kontrol ederiz. Eğer bu koşul sağlanıyorsa (`true`), hareketi engellemek için `move` değişkenini `false` olarak ayarlarız.

Koşulların değerlendirilmesinden sonra, `move` değişkeni kontrol edilir. Eğer `move` `true` ise, tetramina aşağı doğru hareket ettirilir (`currTetramina.movdown()`) ve `currTetraminaDraw()` çağrısı ile güncellenen tetramina tahtaya çizilir. Eğer `move` `false` ise, tetramina hareket etmez ve yalnızca `currTetraminaDraw()` çağrısıyla tetramina tahtaya çizilir.

Ayrıca, hareket mümkün değilse, `currTetraminaDraw()` çağrısıyla tetramina tahtaya çizilir, dolu satırları kontrol etmek için `CheckRows()` çağrılır ve yeni bir tetramina oluşturulur.

```
    public void CurrtetraminamoveRotate()
        {
            Point Position = currTetramina.getCurrPosition();
            Point[] S = new Point[4];
            Point[] Shape = currTetramina.getCurrShape();
            bool move = true;
            Shape.CopyTo(S, 0);
            currTetraminaErase();
            for (int i = 0; i < S.Length; i++)
            {
                double x = S[i].X;
                S[i].X = S[i].Y * -1;
                S[i].Y = x;
                if (((int)((S[i].Y + Position.Y) + 2)) >= Rows)
                {
                    move = false;
                }
                else if (((int)(S[i].X + Position.X) + ((Cols / 2) - 1)) < 0)
                {
                    move = false;
                }
                else if (((int)(S[i].X + Position.X) + ((Cols / 2) - 1)) >= Rows)
                {
                    move = false;
                }
                else if (BlockControls[((int)(S[i].X + Position.X) + ((Cols / 2) - 1)),
                    (int)(S[i].Y + Position.Y) + 2].Background != NoBrush)
                {
                    move = false;
                }
            }
            if (move)
            {
                currTetramina.movrotate();
                currTetraminaDraw();
            }
            else
            {
                currTetraminaDraw();
            }
        }
    }

```   
   Bu `CurrtetraminamoveRotate` adlı yöntem, mevcut tetraminanın döndürme hareketini gerçekleştirir. İşlem sırasında mevcut tetraminanın pozisyonu (`Position`) ve şekli (`Shape`) alınır. Bir dizi koordinat (`S`) oluşturulur ve mevcut şekil `Shape` bu diziye kopyalanır. Daha sonra, mevcut tetramina tahtadan silinir (`currTetraminaErase()` çağrısı).

Döndürme işlemi, her bir şekil noktasının koordinatlarının dönüştürülmesiyle gerçekleştirilir. Her bir noktanın `X` koordinatı `Y` koordinatının negatif değeriyle, `Y` koordinatı ise `X` koordinatıyla değiştirilir. Döndürme işlemi sonrasında, her bir dönüştürülmüş noktanın hareketin mümkün olup olmadığını kontrol etmek için bir dizi koşul denetlenir.

İlk olarak, her bir noktanın tahtanın alt kenarına çarpmayacağını kontrol ederiz. Bunun için, `((int)((S[i].Y + Position.Y) + 2)) >= Rows` koşulunu kontrol ederiz. Eğer bu koşul sağlanıyorsa (`true`), döndürme işlemini engellemek için `move` değişkenini `false` olarak ayarlarız.

Daha sonra, her bir noktanın tahtanın sol veya sağ kenarlarına çarpmayacağını kontrol ederiz. Sol kenar için, `((int)(S[i].X + Position.X) + ((Cols / 2) - 1)) < 0` koşulunu kontrol ederiz. Sağ kenar için ise, `((int)(S[i].X + Position.X) + ((Cols / 2) - 1)) >= Rows` koşulunu kontrol ederiz. Eğer herhangi bir kenara çarpma koşulu sağlanıyorsa (`true`), döndürme işlemini engellemek için `move` değişkenini `false` olarak ayarlarız.

Son olarak, her bir noktanın dönüştürüldükten sonra yeni konumunda başka bir blokla çakışıp çakışmadığını kontrol ederiz. Bunun için, `BlockControls[((int)(S[i].X + Position.X) + ((Cols / 2) - 1)), (int)(S[i].Y + Position.Y) + 2].Background != NoBrush` koşulunu kontrol ederiz. Eğer bu koşul sağlanıyorsa (`true`), döndürme işlemini engellemek için `move` değişkenini `false` olarak ayarlarız.

Koşulların değerlendirilmesinden sonra, `move` değişkeni kontrol edilir. Eğer `move` `true` ise, tetramina döndürülür (`currTetramina.movrotate()`) ve `currTetraminaDraw()` çağrısı ile gü

ncellenmiş tetramina tahta üzerine çizilir. Eğer `move` `false` ise, tetramina sadece çizilir ve döndürme işlemi gerçekleştirilmez.

Bu şekilde, `CurrtetraminamoveRotate` yöntemi mevcut tetraminanın döndürme hareketini gerçekleştirir.
açık olcak şekilde yeni tetramina adında bir sınıf oluşturuyoruz.Burda yukardan incek şekilleri oluşturcaz.
   ```
   private Point currPosition;
        private Point[] currShape;
        private Brush currColor;
        private bool rotate;
   ```
   gerekli değişkenleri oluşturalım.
   ```
   public void movrotate()
        {
            if (rotate)
            {
                for (int i = 0; i < currShape.Length; i++)
                {
                    double x = currShape[i].X;
                    currShape[i].X = currShape[i].Y * -1;
                    currShape[i].Y = x;
                }

            }
        }
   ```
                                                     Bu `movrotate` adlı yöntem, mevcut tetraminanın döndürme işlemi gerçekleştirmek için kullanılır. İşlem sırasında tetraminanın şeklini temsil eden `currShape` dizisi üzerinde döngü oluşturulur. Her bir şekil noktasının `X` ve `Y` koordinatları değiştirilerek dönüş işlemi gerçekleştirilir.

Döndürme işlemi için her bir noktanın `X` ve `Y` koordinatları değiştirilir. Geçici bir değişken olan `x` oluşturulur ve mevcut noktanın `X` değeri bu değişkene atanır. Ardından, `X` değeri `Y` değerinin negatifini alır ve `Y` değeri `x` değerine atanır. Bu işlem, noktanın saat yönünün tersine dönmesini sağlar.

Döndürme işlemi yalnızca `rotate` değeri `true` olduğunda gerçekleştirilir. Bu, döndürmenin etkinleştirildiği bir kontrol mekanizmasıdır. Eğer `rotate` `false` ise, döndürme işlemi atlanır ve mevcut tetraminanın şekli değişmez.

Böylece, `movrotate` yöntemi mevcut tetraminanın döndürme işlemini gerçekleştirir.
```
private Point[] SetRandomShape()
        {
            Random rand = new Random();
            switch (rand.Next() % 7)
            {//kordinat düzleminde noktalar olşuturup şekil elde edicez.
                case 0://I
                    rotate = true;
                    currColor = Brushes.Cyan;
                    return new Point[]
                    {
                        new Point(0,0),
                        new Point(-1,0),
                        new Point(1,0),
                        new Point(2,0)
                    };
                case 1://J
                    rotate = true;
                    currColor = Brushes.Blue;
                    return new Point[]
                    {
                        new Point(1,-1),
                        new Point(-1,0),
                        new Point(0,0),
                        new Point(1,0)
                    };
```
 toplamda 7 farklı seçenek olcak hepsini bura yazmadım.Bu `SetRandomShape` adlı yöntem, rastgele bir tetramina şekli oluşturmak için kullanılır. `Random` sınıfı kullanılarak bir rastgele sayı oluşturulur ve `switch` ifadesi kullanılarak farklı şekiller oluşturulur.

Her bir `case` durumunda, tetraminanın dönme özelliği (`rotate`) ve rengi (`currColor`) belirlenir. Ardından, `Point` türünde bir dizi döndürülür. Bu dizi, tetraminanın şeklini temsil eden noktaları içerir.

Örneğin, `case 0` durumunda (I şekli), tetraminanın dönme özelliği `true` olarak ayarlanır ve `currColor` rengi `Cyan` olarak belirlenir. Daha sonra, I şeklini oluşturan dört nokta (`Point`) içeren bir dizi döndürülür.

`case 1` durumunda (J şekli) ise, tetraminanın dönme özelliği yine `true` olarak ayarlanır ve `currColor` rengi `Blue` olarak belirlenir. J şeklini oluşturan dört noktayı içeren bir dizi döndürülür.

Diğer durumlar için de benzer şekilde tetraminanın şekli, dönme özelliği ve renkleri belirlenir ve ilgili dizi döndürülür.

Bu yöntem, rastgele bir tetraminanın şeklini ve özelliklerini belirlemek için kullanılır.
```
                                           public partial class MainWindow : Window
    {
        DispatcherTimer Timer;
        board myBoard;
        public MainWindow()
        {
            InitializeComponent();
        }
        void MainWindow_Initilized(object sender, EventArgs e)
        {
            Timer = new DispatcherTimer();
            Timer.Tick += new EventHandler(GameTick);
            Timer.Interval = new TimeSpan(0, 0, 0, 0, 400);
            GameStart();
        }
        private void GameStart()
        {
            MainGrid.Children.Clear();
            myBoard = new board(MainGrid);
            Timer.Start();
        }
        void GameTick(object sender, EventArgs e)
        {
            Scores.Content = myBoard.getScore().ToString("0000000000");
            Lines.Content = myBoard.getLines().ToString("0000000000");
            myBoard.CurrtetraminamoveDown();
        }
        private void GamePause()
        {
            if (Timer.IsEnabled) Timer.Stop();
            else Timer.Start();
        }
        private void HandleKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    if(Timer.IsEnabled) myBoard.CurrtetraminamoveLeft();
                    break;
                case Key.Right:
                    if (Timer.IsEnabled) myBoard.CurrtetraminamoveRight();
                    break;
                case Key.Down:
                    if (Timer.IsEnabled) myBoard.CurrtetraminamoveDown();
                    break;
                case Key.Up:
                    if (Timer.IsEnabled) myBoard.CurrtetraminamoveRotate();
                    break;
                case Key.F2:
                    GameStart();
                    break;
                case Key.F3:
                    GamePause();
                    break;
                default:
                    break;
            }
        }  
    }
}
                                                    ```
                                                     
                                                     Bu kod parçası `MainWindow` sınıfını tanımlar ve Tetris oyununun temel işlevselliğini içerir.

`MainWindow` sınıfı, `Window` sınıfından türetilmiş ve `partial` olarak işaretlenmiştir. Bu sınıf, Tetris oyununun ana penceresini temsil eder.

Sınıfın özellikleri şunlardır:

- `DispatcherTimer Timer`: Oyunun zamanlayıcısını temsil eder. Tetris in sürekli olarak hareket etmesini sağlar.
- `board myBoard`: Oyun tahtasını temsil eden `board` sınıfından bir örnek.
- `Scores` ve `Lines` adlı `ContentControl` öğeleri: Oyundaki skoru ve doldurulan satır sayısını görüntülemek için kullanılır.

Sınıfın yöntemleri şunlardır:

- `MainWindow()`: Sınıfın yapıcı yöntemi. İçerisinde `InitializeComponent()` yöntemi çağrılır.
- `MainWindow_Initilized(object sender, EventArgs e)`: Pencere başlatıldığında tetris oyununu başlatmak için kullanılan bir olay işleyici yöntemidir. Bu yöntem, bir `DispatcherTimer` oluşturur, `GameStart()` yöntemini çağırır ve zamanlayıcıyı başlatır.
- `GameStart()`: Oyunu başlatan yöntem. `MainGrid` içeriğini temizler, yeni bir `board` örneği oluşturur ve zamanlayıcıyı başlatır.
- `GameTick(object sender, EventArgs e)`: Oyun zamanlayıcının tetiklediği olay işleyici yöntemidir. Her zamanlayıcı tetiklendiğinde, skor ve doldurulan satır sayısı güncellenir ve `myBoard.CurrtetraminamoveDown()` yöntemi çağrılarak mevcut tetraminanın aşağı hareketi gerçekleştirilir.
- `GamePause()`: Oyunu duraklatan veya devam ettiren yöntem. Eğer zamanlayıcı aktifse durdurur, duraksaysa devam ettirir.
- `HandleKeyDown(object sender, KeyEventArgs e)`: Klavye tuşlarına yanıt veren olay işleyici yöntemidir. Tuşa basıldığında, ilgili işlemleri gerçekleştirir. Örneğin, sol tuşa basıldığında `myBoard.CurrtetraminamoveLeft()` yöntemi çağrılarak tetraminanın sola hareketi gerçekleştirilir. F2 tuşuna basıldığında oyun yeniden başlar, F3 tuşuna basıldığında oyun duraklatılır.

Bu kod parçası, Tetris oyununun ana penceresi için gerekli olan olay işleyicileri, yöntemleri ve temel işlevselliği içermektedir.Son dokunuşuda yaptık oyun hazır artık oynanabilir.


