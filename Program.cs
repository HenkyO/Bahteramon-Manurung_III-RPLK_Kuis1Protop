using System;
using System.Collections.Generic;
using System.Linq;

// Kelas untuk merepresentasikan produk kopi
class Produk
{
    public string Nama { get; set; }
    public double Harga { get; set; }
    public int Stok { get; set; }

    public Produk(string nama, double harga, int stok)
    {
        Nama = nama;
        Harga = harga;
        Stok = stok;
    }

    public override string ToString()
    {
        return $"{Nama}\tHarga: {Harga:C}\tStok: {Stok}";
    }
}

// Kelas untuk mengelola daftar produk
class PengelolaProduk
{
    private List<Produk> produk;

    public PengelolaProduk()
    {
        produk = new List<Produk>();
        // Contoh produk awal
        produk.Add(new Produk("Arabika", 10000, 100));
        produk.Add(new Produk("Robusta", 8000, 80));
        produk.Add(new Produk("Luwak", 50000, 20));
    }

    public void TambahProduk(Produk produkBaru)
    {
        produk.Add(produkBaru);
    }

    public void HapusProduk(string nama)
    {
        produk.RemoveAll(p => p.Nama.Equals(nama, StringComparison.OrdinalIgnoreCase));
    }

    public List<Produk> CariBerdasarkanNama(string nama)
    {
        return produk.Where(p => p.Nama.ToLower().Contains(nama.ToLower())).ToList();
    }

    public List<Produk> FilterBerdasarkanHarga(double hargaMin, double hargaMax)
    {
        return produk.Where(p => p.Harga >= hargaMin && p.Harga <= hargaMax).ToList();
    }

    public void UrutBerdasarkanStok()
    {
        produk.Sort((p1, p2) => p2.Stok.CompareTo(p1.Stok));
    }

    public void TampilkanProduk()
    {
        foreach (var p in produk)
        {
            Console.WriteLine(p);
        }
    }
}

// Kelas untuk aplikasi penjualan
class Program
{
    static void Main(string[] args)
    {
        PengelolaProduk pengelolaProduk = new PengelolaProduk();
        string username, password;

        // Login
        do
        {
            Console.Write("Masukkan nama pengguna: ");
            username = Console.ReadLine();
            Console.Write("Masukkan kata sandi: ");
            password = Console.ReadLine();
        } while (username != "admin" || password != "1234");

        Console.WriteLine("Login berhasil.");

        // Menu
        int pilihan;
        do
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Cari Produk");
            Console.WriteLine("2. Filter Produk berdasarkan Harga");
            Console.WriteLine("3. Urutkan Produk berdasarkan Stok");
            Console.WriteLine("4. Tambah Produk Baru");
            Console.WriteLine("5. Hapus Produk");
            Console.WriteLine("6. Tampilkan Semua Produk");
            Console.WriteLine("7. Keluar");
            Console.Write("Masukkan pilihan Anda: ");
            pilihan = int.Parse(Console.ReadLine());

            switch (pilihan)
            {
                case 1:
                    Console.Write("Masukkan nama produk yang ingin dicari: ");
                    string namaCari = Console.ReadLine();
                    List<Produk> hasilPencarian = pengelolaProduk.CariBerdasarkanNama(namaCari);
                    if (hasilPencarian.Any())
                    {
                        Console.WriteLine("Hasil Pencarian:");
                        foreach (var p in hasilPencarian)
                        {
                            Console.WriteLine(p);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Tidak ditemukan produk dengan nama tersebut.");
                    }
                    break;
                case 2:
                    Console.Write("Masukkan harga minimum: ");
                    double hargaMin = double.Parse(Console.ReadLine());
                    Console.Write("Masukkan harga maksimum: ");
                    double hargaMax = double.Parse(Console.ReadLine());
                    List<Produk> produkTersaring = pengelolaProduk.FilterBerdasarkanHarga(hargaMin, hargaMax);
                    if (produkTersaring.Any())
                    {
                        Console.WriteLine("Produk Tersaring:");
                        foreach (var p in produkTersaring)
                        {
                            Console.WriteLine(p);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Tidak ditemukan produk dalam rentang harga tersebut.");
                    }
                    break;
                case 3:
                    pengelolaProduk.UrutBerdasarkanStok();
                    Console.WriteLine("Produk diurutkan berdasarkan stok:");
                    pengelolaProduk.TampilkanProduk();
                    break;
                case 4:
                    Console.Write("Masukkan nama produk: ");
                    string namaProdukBaru = Console.ReadLine();
                    Console.Write("Masukkan harga produk: ");
                    double hargaProdukBaru = double.Parse(Console.ReadLine());
                    Console.Write("Masukkan stok produk: ");
                    int stokProdukBaru = int.Parse(Console.ReadLine());
                    pengelolaProduk.TambahProduk(new Produk(namaProdukBaru, hargaProdukBaru, stokProdukBaru));
                    Console.WriteLine("Produk berhasil ditambahkan.");
                    break;
                case 5:
                    Console.Write("Masukkan nama produk yang ingin dihapus: ");
                    string produkYangAkanDihapus = Console.ReadLine();
                    pengelolaProduk.HapusProduk(produkYangAkanDihapus);
                    Console.WriteLine("Produk berhasil dihapus.");
                    break;
                case 6:
                    Console.WriteLine("Semua Produk:");
                    pengelolaProduk.TampilkanProduk();
                    break;
                case 7:
                    Console.WriteLine("Keluar...");
                    break;
                default:
                    Console.WriteLine("Pilihan tidak valid. Silakan coba lagi.");
                    break;
            }
        } while (pilihan != 7);
    }
}
