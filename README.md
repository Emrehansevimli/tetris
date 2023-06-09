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
<sub> * public board(Grid TetrisGrid)
       * {
        *    Rows = TetrisGrid.RowDefinitions.Count;
         *   Cols = TetrisGrid.ColumnDefinitions.Count;
          *  Score = 0;
          *  BlockControls = new System.Windows.Controls.Label[Cols, Rows];
            for (int i = 0; i < Cols; i++)
           * {
           *     for (int j = 0; j < Rows; j++)
             *   {
              *      BlockControls[i, j] = new System.Windows.Controls.Label();
               *     BlockControls[i, j].Background = NoBrush;
                *    BlockControls[i, j].BorderBrush = SilverBrush;
                 *   BlockControls[i, j].BorderThickness = new Thickness(1, 1, 1, 1);
                 *   Grid.SetRow(BlockControls[i, j], j);
                  *  Grid.SetColumn(BlockControls[i, j], i);
                  *  TetrisGrid.Children.Add(BlockControls[i, j]);
              * }
           * }
          *  currTetramina = new Tetramina();
           * currTetraminaDraw();
        *}</sub>
   Bu kod bloğu, `board` sınıfının yapıcı (constructor) metodu olarak tanımlanmıştır. Bu metot, `TetrisGrid` adında bir `Grid` kontrolü alır ve tetris oyun tahtasını oluşturmak için kullanılır.

İlk olarak, `Rows` ve `Cols` değişkenleri `TetrisGrid` kontrolünün `RowDefinitions` ve `ColumnDefinitions` koleksiyonlarının sayılarını alarak belirlenir. Bu sayılar, tetris tahtasının satır ve sütun sayılarını temsil eder.

Sonra, `Score` değişkeni sıfırlanır. `BlockControls` adında bir `Label` dizisi oluşturulur. Bu dizi, tetris tahtasının her bir bloğunu temsil eder. Dizinin boyutu `Cols` ve `Rows` değerlerine göre belirlenir.

Daha sonra, iki döngü kullanılarak `BlockControls` dizisinin her bir elemanı için `Label` nesneleri oluşturulur. Her bir `Label` nesnesi için arka plan (`Background`) özelliği `NoBrush` değeri ile ayarlanır. `BorderBrush` özelliği `SilverBrush` değeriyle, `BorderThickness` özelliği ise 1 birim kalınlığında bir kenarlık olacak şekilde ayarlanır. Ardından, `Grid.SetRow` ve `Grid.SetColumn` metotlarıyla her bir `Label` nesnesi için doğru satır ve sütun numarası belirlenir ve ilgili `Label` nesnesi `TetrisGrid.Children` koleksiyonuna eklenir. Böylece, her bir blok, `TetrisGrid` kontrolü içindeki doğru konuma yerleştirilmiş olur.

Döngüler tamamlandıktan sonra, `currTetramina` adında yeni bir `Tetramina` nesnesi oluşturulur ve `currTetraminaDraw` metodu çağrılarak bu tetraminanın çizimi yapılır. Bu işlem, oyun tahtasında başlangıçta aktif olan tetraminanın görüntülenmesini sağlar.










