using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Zirve503Lib;

namespace ZirveTicariApi_Test
{
    public partial class TicariApiTestForm : Form
    {
        public Zirve503Islemler ZirveConnect;

        public TicariApiTestForm()
        {
            InitializeComponent();
            //tbSqlAdi.Text = @"DESKTOP-MPSKH68\SQL2008";
            //tbMusteriNo.Text = "922229";
            //tbKlavuzAdi.Text = "ZİRVE_YAZILIM";
            //tbCariKodu.Text = "";
            //tbSifre.Text = "sifre";
            //tbKullaniciAdi.Text = "zirvenet";
            //tbYil.Text = "2016";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //
            ZirveConnect = new Zirve503Islemler();
            ZirveConnect.set_SqlAdi(tbSqlAdi.Text);  //"V3772G\\SQL2008"
            ZirveConnect.set_SqlKullaniciAdi(tbKullaniciAdi.Text); //"zirvenet"
            ZirveConnect.set_SqlSifre(tbSifre.Text); //"zrvsql"
            ZirveConnect.set_FirmaKlavuzAdi(tbKlavuzAdi.Text); //"ZRVLIB_TEST"
            ZirveConnect.set_CalismaYili(tbYil.Text); //"2015"
            ZirveConnect.set_MusteriNo(tbMusteriNo.Text);
            ZirveConnect.SqleBaglan();

            CbkIslem cbkIslem = new CbkIslem(ZirveConnect);

            cbkIslem.set_EvrakTuru(DekontTuru.CariTahsilFisi);

            cbkIslem.set_Tarih(dateTimePicker1.Value);
            cbkIslem.set_Saat(dateTimePicker1.Value);
            cbkIslem.set_EvrakNo(tbEvrakNo.Text);
            cbkIslem.set_DovizKodu("TL");

            cbkIslem.set_TutarTL(100f);

             KasaKart kasaKart = cbkIslem.get_KasaKart();
             kasaKart.set_KartKodu("Kasa01");
             if (string.IsNullOrEmpty(kasaKart.get_KartAdi()))
             {
                 kasaKart.set_KartAdi("Deneme Kasa");
             }
             //GiderKarti giderKart = cbkIslem.get_GiderKart();
             //   giderKart.set_KartKodu(tbMasrafKodu.Text);
             //   giderKart.set_KartAdi(tbMasrafAdi.Text);

             CariKart cariKart = cbkIslem.get_CariKart();
             cariKart.set_KartKodu("ZG-11223344556");
             if (string.IsNullOrEmpty(cariKart.get_KartAdi()))
             {
                 cariKart.set_KartAdi("Fahrettin Eroglu");
             }
            
            cbkIslem.set_Aciklama("Açıklama");
            cbkIslem.set_OzelKod1(tbOzelKod1.Text);
            cbkIslem.set_OzelKod2(tbOzelKod2.Text);

            if (cbkIslem.Dekont_Kaydet())
            {
                MessageBox.Show("Dekont kaydı tamamlanmıştır !...","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Dekont kaydı oluşturulamamıştır !..." +
                    Environment.NewLine + cbkIslem.get_HataMesaji(),"Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Fatura fatura = null;

            try
            {
                ZirveConnect = new Zirve503Islemler();
                ZirveConnect.set_SqlAdi(tbSqlAdi.Text);  //"V3772G\\SQL2008"
                ZirveConnect.set_SqlKullaniciAdi(tbKullaniciAdi.Text); //"zirvenet"
                ZirveConnect.set_SqlSifre(tbSifre.Text); //"zrvsql"
                ZirveConnect.set_FirmaKlavuzAdi(tbKlavuzAdi.Text); //"ZRVLIB_TEST"
                ZirveConnect.set_CalismaYili(tbYil.Text); //"2015"
                ZirveConnect.set_MusteriNo(tbMusteriNo.Text);
                ZirveConnect.SqleBaglan();

                fatura = new Fatura(ZirveConnect);

                fatura.set_Turu(FaturaTuru.SatisFaturasi);
                fatura.set_EvrakNo(tbEvrakNo.Text);
                fatura.set_Tarih(dateTimePicker1.Value);
                fatura.set_Saat(dateTimePicker1.Value);
                fatura.set_DovizKodu("TL");

                if (!string.IsNullOrEmpty(tbTutarTL.Text))
                {
                    fatura.set_GenelToplam(float.Parse(tbTutarTL.Text));
                }
                else
                fatura.set_GenelToplam(100f);

                if (!string.IsNullOrEmpty(tbKDV.Text))
                {
                    fatura.set_ToplamKDV(float.Parse(tbKDV.Text));
                }
                else
                fatura.set_ToplamKDV(18f);

                CariKart cariKart = fatura.get_CariKart();
                if (!string.IsNullOrEmpty(tbCariKodu.Text))
                {
                    cariKart.set_KartKodu(tbCariKodu.Text);
                    if (string.IsNullOrEmpty(cariKart.get_KartAdi()))
                        cariKart.set_KartAdi(tbCariAdi.Text);
                }

                KasaKart kasaKart = fatura.get_KasaKart();
                BankaKart bankaKart = fatura.get_BankaKart();
                if (!string.IsNullOrEmpty(tbKasaKodu.Text))
                {
                    kasaKart.set_KartKodu(tbKasaKodu.Text);
                    if (string.IsNullOrEmpty(kasaKart.get_KartAdi()))
                        kasaKart.set_KartAdi(tbKasaAdi.Text);
                }
                else
                    if (!string.IsNullOrEmpty(tbBankaKodu.Text))
                    {
                        bankaKart.set_KartKodu(tbBankaKodu.Text);
                        if (string.IsNullOrEmpty(bankaKart.get_KartAdi()))
                            bankaKart.set_KartAdi(tbBankaAdi.Text);
                    }

                fatura.set_Aciklama(tbAciklama.Text);

                fatura.set_Ozelkod1(tbOzelKod1.Text);
                fatura.set_Ozelkod2(tbOzelKod2.Text);

                fatura.set_SubeNo(1);
                fatura.set_DepoKodu("MERKEZ");

                ObservableCollection<FaturaSatir> FaturaSatirlari = fatura.get_Stoklar(); // new ObservableCollection<FaturaSatir>();

                FaturaSatirlari.Add(new FaturaSatir(ZirveConnect));

                FaturaSatir satir1 = FaturaSatirlari[0]; // fatura.Stoklar[0];

                satir1.set_SatirTuru(SatirTuru.Stok);
                satir1.set_KartKodu(tbStokKodu1.Text);
                if (string.IsNullOrEmpty(satir1.get_KartAdi()))
                {
                    satir1.set_KartAdi("VODAFONE 875 SMART");
                }

                if (!string.IsNullOrEmpty(tbMiktar1.Text))
                {
                    satir1.set_Miktar(float.Parse(tbMiktar1.Text));
                }
                else
                satir1.set_Miktar(10f);

                satir1.set_DovizKodu("TL");
                if (!string.IsNullOrEmpty(tbBrfTL1.Text))
                {
                    satir1.set_BirimFiyatTL(float.Parse(tbBrfTL1.Text));
                }
                else
                satir1.set_BirimFiyatTL(14f);

                satir1.set_KDVOrani(18);
                satir1.set_IndirimOrani(0f);
                if (string.IsNullOrEmpty(satir1.get_Birim()))
                {
                    satir1.set_Birim("ADET");
                }
                satir1.set_Satir_Id(Guid.NewGuid().ToString());
                satir1.set_Satir_Sirano(1);

                satir1.set_TevkifatOrani("2/10");
                satir1.set_TEVKTL(9f);

                if (!string.IsNullOrEmpty(tbStokKodu2.Text))
                {
                    FaturaSatirlari.Add(new FaturaSatir(ZirveConnect));
                    FaturaSatir satir2 = FaturaSatirlari[1];
                    satir2.set_SatirTuru(SatirTuru.Stok);

                    satir2.set_KartKodu(tbStokKodu2.Text);
                    if (string.IsNullOrEmpty(satir2.get_KartAdi()))
                    {
                        satir2.set_KartAdi("ARAÇ GİDERLERİ");
                    }

                    if (!string.IsNullOrEmpty(tbMiktar2.Text))
                    {
                        satir2.set_Miktar(float.Parse(tbMiktar2.Text));
                    }
                    else
                    satir2.set_Miktar(3f);
                    satir2.set_DovizKodu("TL");

                    if (!string.IsNullOrEmpty(tbBrftl2.Text))
                    {
                        satir2.set_BirimFiyatTL(float.Parse(tbBrftl2.Text));
                    }
                    else
                    satir2.set_BirimFiyatTL(8f);
                    satir2.set_KDVOrani(18);
                    satir2.set_IndirimOrani(10f);
                    if (string.IsNullOrEmpty(satir2.get_Birim()))
                    {
                        satir2.set_Birim("ADET");
                    }
                    satir2.set_Satir_Id(Guid.NewGuid().ToString());
                    satir2.set_Satir_Sirano(2);

                }

                
                fatura.set_EFatura(true);
                // Not sadece Satış ve Satış iade faturalarında geçerlidir.
                fatura.set_EFaturaEvraknoVerilsin(true);


                //fatura.set_IrsaliyeTarihi(new DateTime(2016, 3, 28));
                //fatura.set_IrsaliyeNo("Irs001");

                
                if (fatura.Fatura_Kaydet())
                {
                    MessageBox.Show("Fatura kaydı tamamlanmıştır !..." + Environment.NewLine+
                     "Kaydedilen evraknno :"+fatura.get_KayitSonucu().get_Evrakno(), "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Fatura kaydı oluşturulamamıştır !..." +
                        Environment.NewLine +
                        fatura.get_HataMesaji()+
                      Environment.NewLine+
                      "Kayıt sonucu : "+fatura.get_KayitSonucu().get_HataMesaji() , "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                string eklemesaj = string.Empty;
                if (fatura != null)
                    eklemesaj = fatura.get_HataMesaji();

                MessageBox.Show(ex.Message + Environment.NewLine + eklemesaj);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                ZirveConnect = new Zirve503Islemler();

                ZirveConnect.set_MusteriNo(tbMusteriNo.Text);

                tbKeyInfo.Text = ZirveConnect.BilgisayarKeyOlustur();

                MessageBox.Show(tbKeyInfo.Text);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            //MessageBox.Show(ZirveConnect.BilgisayarKeyOlustur());

        }

        private void button4_Click(object sender, EventArgs e)
        {
            ZirveConnect = new Zirve503Islemler();

            ZirveConnect.set_MusteriNo(tbMusteriNo.Text);

            MessageBox.Show(ZirveConnect.ProgramKeyKaydet(tbKeyInfo.Text));

            //MessageBox.Show(ZirveConnect.ProgramKeyKaydet("d2v8xda86ec956ebJad62d4149d35ab7f59bee79179115ffec503cc9a3976051"));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ZirveConnect = new Zirve503Islemler();
            ZirveConnect.set_SqlAdi(@"DESKTOP-VPL4FFP\SQL2008");  //"V3772G\\SQL2008"
            ZirveConnect.set_SqlKullaniciAdi("zirvenet"); //"zirvenet"
            ZirveConnect.set_SqlSifre("zrvsql"); //"zrvsql"
            ZirveConnect.set_FirmaKlavuzAdi("MÜLLER_KİMYA"); //"ZRVLIB_TEST"
            ZirveConnect.set_CalismaYili("2014"); //"2015"
            ZirveConnect.set_MusteriNo("922229");
            ZirveConnect.SqleBaglan();

            Siparis siparis = new Siparis(ZirveConnect);

            siparis.set_Turu(SiparisTuru.AlinanSiparis);

            siparis.set_EvrakNo(tbEvrakNo.Text);
            siparis.set_Tarih(dateTimePicker1.Value);
            siparis.set_Saat(dateTimePicker1.Value);

            siparis.set_DovizKodu("TL");

            siparis.set_GenelToplam(345f);
            siparis.set_ToplamKDV(23f);

            CariKart cariKart = siparis.get_CariKart();
            if (!string.IsNullOrEmpty(tbCariKodu.Text))
            {
                cariKart.set_KartKodu(tbCariKodu.Text);
                if (string.IsNullOrEmpty(cariKart.get_KartAdi()))
                    cariKart.set_KartAdi(tbCariAdi.Text);
            }

            siparis.set_Aciklama(tbAciklama.Text);

            siparis.set_Ozelkod1(tbOzelKod1.Text);
            siparis.set_Ozelkod2(tbOzelKod2.Text);

            siparis.set_SubeNo(1);
            siparis.set_DepoKodu("MERKEZ");

            //ObservableCollection<SiparisSatir> SiparisSatirlari = siparis.get_Stoklar();
            ObservableCollection<SiparisSatir> SiparisSatirlari = siparis.get_Stoklar(); // new ObservableCollection<FaturaSatir>();

            decimal bakiye = 0;

            if (!string.IsNullOrEmpty(tbStokKodu1.Text))
            {
                bakiye = siparis.StokBakiyesi_Ver(tbStokKodu1.Text);

                if (bakiye > 0 | siparis.get_BakiyeSorma())
                {

                    
                    SiparisSatirlari.Add(new SiparisSatir(ZirveConnect));

                    SiparisSatir satir1 = SiparisSatirlari[0]; // SiparisSatirlari[0];

                    satir1.set_KartKodu(tbStokKodu1.Text);
                    if (string.IsNullOrEmpty(satir1.get_KartAdi()))
                        satir1.set_KartAdi("Kalem");
                    satir1.set_Miktar(10f);
                    satir1.set_DovizKodu("TL");
                    satir1.set_BirimFiyatTL(10f);
                    satir1.set_KDVOrani(18);
                    satir1.set_IndirimOrani(10);
                    satir1.set_Birim("ADET");
                    satir1.set_Satir_Id(Guid.NewGuid().ToString());
                    satir1.set_Satir_Sirano(1);

                }
            }

            if (!string.IsNullOrEmpty(tbStokKodu2.Text))
            {
                bakiye = siparis.StokBakiyesi_Ver(tbStokKodu2.Text);

                if (bakiye > 0 | siparis.get_BakiyeSorma())
                {
                    SiparisSatirlari.Add(new SiparisSatir(ZirveConnect));

                    SiparisSatir satir2 = SiparisSatirlari[1]; // SiparisSatirlari[0];

                    satir2.set_KartKodu(tbStokKodu2.Text);
                    if (string.IsNullOrEmpty(satir2.get_KartAdi()))
                        satir2.set_KartAdi("Kalem");
                    satir2.set_Miktar(5f);
                    satir2.set_DovizKodu("TL");
                    satir2.set_BirimFiyatTL(12f);
                    satir2.set_KDVOrani(18);
                    satir2.set_IndirimOrani(0);
                    satir2.set_Birim("ADET");
                    satir2.set_Satir_Id(Guid.NewGuid().ToString());
                    satir2.set_Satir_Sirano(2);
                }
            }


            if (SiparisSatirlari.Count > 0)
            {
                if (siparis.Siparis_Kaydet())
                {
                    MessageBox.Show("Sipariş kaydı tamamlanmıştır !...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Siparis kaydı oluşturulamamıştır !..." +
                        Environment.NewLine +
                        siparis.get_HataMesaji(), "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ZirveConnect = new Zirve503Islemler();
            ZirveConnect.set_MusteriNo(tbMusteriNo.Text);

            if (ZirveConnect.get_LisansVarmi())
            {
                MessageBox.Show("Lisans Kaydı yapılmış");
            }
            else
            {
                MessageBox.Show("Lisans kaydı sorunlu");
            }
                
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Fatura fatura = null;

            try
            {
                ZirveConnect = new Zirve503Islemler();
                ZirveConnect.set_SqlAdi(tbSqlAdi.Text);  //"V3772G\\SQL2008"
                ZirveConnect.set_SqlKullaniciAdi(tbKullaniciAdi.Text); //"zirvenet"
                ZirveConnect.set_SqlSifre(tbSifre.Text); //"zrvsql"
                ZirveConnect.set_FirmaKlavuzAdi(tbKlavuzAdi.Text); //"ZRVLIB_TEST"
                ZirveConnect.set_CalismaYili(tbYil.Text); //"2015"
                ZirveConnect.set_MusteriNo(tbMusteriNo.Text);
                ZirveConnect.SqleBaglan();

                fatura = new Fatura(ZirveConnect);

                fatura.set_Turu(FaturaTuru.AlisFaturasi);
                fatura.set_EvrakNo(tbEvrakNo.Text);
                fatura.set_Tarih(dateTimePicker1.Value);
                fatura.set_Saat(dateTimePicker1.Value);
                fatura.set_DovizKodu("TL");

                fatura.set_GenelToplam (100f);
                fatura.set_ToplamKDV(18f);

                CariKart cariKart = fatura.get_CariKart();
                if (!string.IsNullOrEmpty(tbCariKodu.Text))
                {
                    cariKart.set_KartKodu(tbCariKodu.Text);
                    if (string.IsNullOrEmpty(cariKart.get_KartAdi()))
                        cariKart.set_KartAdi(tbCariAdi.Text);
                }

                KasaKart kasaKart = fatura.get_KasaKart();
                BankaKart bankaKart = fatura.get_BankaKart();

                if (!string.IsNullOrEmpty(tbKasaKodu.Text))
                {
                    kasaKart.set_KartKodu(tbKasaKodu.Text);
                    if (string.IsNullOrEmpty(kasaKart.get_KartAdi()))
                        kasaKart.set_KartAdi(tbKasaAdi.Text);
                }
                else
                    if (!string.IsNullOrEmpty(tbBankaKodu.Text))
                    {
                        bankaKart.set_KartKodu(tbBankaKodu.Text);
                        if (string.IsNullOrEmpty(bankaKart.get_KartAdi()))
                            bankaKart.set_KartAdi(tbBankaAdi.Text);
                    }

                fatura.set_Aciklama(tbAciklama.Text);

                fatura.set_Ozelkod1(tbOzelKod1.Text);
                fatura.set_Ozelkod2(tbOzelKod2.Text);

                fatura.set_SubeNo(1);
                fatura.set_DepoKodu("MERKEZ");

                ObservableCollection<FaturaSatir> FaturaSatirlari = fatura.get_Stoklar(); // new ObservableCollection<FaturaSatir>();

                FaturaSatirlari.Add(new FaturaSatir(ZirveConnect));

                FaturaSatir satir1 = FaturaSatirlari[0]; // fatura.Stoklar[0];

                satir1.set_SatirTuru(SatirTuru.Masraf);
                satir1.set_KartKodu("760.01.201");
                satir1.set_KartAdi("ARAÇ GİDERLERİ");
                satir1.set_Miktar(1f);
                satir1.set_DovizKodu("TL");
                satir1.set_BirimFiyatTL(14f);
                satir1.set_KDVOrani(18);
                satir1.set_IndirimOrani(0f);
                satir1.set_Birim("ADET");
                satir1.set_Satir_Id(Guid.NewGuid().ToString());
                satir1.set_Satir_Sirano(1);

                if (fatura.Fatura_Kaydet())
                {
                    MessageBox.Show("Fatura kaydı tamamlanmıştır !...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Fatura kaydı oluşturulamamıştır !..." +
                        Environment.NewLine +
                        fatura.get_HataMesaji(), "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                string eklemesaj = string.Empty;
                if (fatura != null)
                    eklemesaj = fatura.get_HataMesaji();

                MessageBox.Show(ex.Message + Environment.NewLine + eklemesaj);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Fatura fatura = null;

            try
            {
                ZirveConnect = new Zirve503Islemler();
                ZirveConnect.set_SqlAdi(tbSqlAdi.Text);  //"V3772G\\SQL2008"
                ZirveConnect.set_SqlKullaniciAdi(tbKullaniciAdi.Text); //"zirvenet"
                ZirveConnect.set_SqlSifre(tbSifre.Text); //"zrvsql"
                ZirveConnect.set_FirmaKlavuzAdi(tbKlavuzAdi.Text); //"ZRVLIB_TEST"
                ZirveConnect.set_CalismaYili(tbYil.Text); //"2015"
                ZirveConnect.set_MusteriNo(tbMusteriNo.Text);
                ZirveConnect.SqleBaglan();

                fatura = new Fatura(ZirveConnect);

                fatura.set_Turu(FaturaTuru.AlisFaturasi);
                fatura.set_EvrakNo(tbEvrakNo.Text);
                fatura.set_Tarih(dateTimePicker1.Value);
                fatura.set_Saat(dateTimePicker1.Value);
                fatura.set_DovizKodu("TL");

                fatura.set_GenelToplam(100f);
                fatura.set_ToplamKDV(18f);

                CariKart cariKart = fatura.get_CariKart();
                if (!string.IsNullOrEmpty(tbCariKodu.Text))
                {
                    cariKart.set_KartKodu(tbCariKodu.Text);
                    if (string.IsNullOrEmpty(cariKart.get_KartAdi()))
                        cariKart.set_KartAdi(tbCariAdi.Text);
                }

                fatura.set_Aciklama(tbAciklama.Text);

                fatura.set_Ozelkod1(tbOzelKod1.Text);
                fatura.set_Ozelkod2(tbOzelKod2.Text);

                fatura.set_SubeNo(1);
                fatura.set_DepoKodu("MERKEZ");

                ObservableCollection<FaturaSatir> FaturaSatirlari = fatura.get_Stoklar(); // new ObservableCollection<FaturaSatir>();

                FaturaSatirlari.Add(new FaturaSatir(ZirveConnect));

                FaturaSatir satir1 = FaturaSatirlari[0]; // fatura.Stoklar[0];

                satir1.set_SatirTuru(SatirTuru.Masraf);
                satir1.set_KartKodu("TEST 2");
                satir1.set_KartAdi("TEST 2");
                satir1.set_Miktar(1f);
                satir1.set_DovizKodu("TL");
                satir1.set_BirimFiyatTL(14f);
                satir1.set_KDVOrani(18);
                satir1.set_IndirimOrani(0f);
                satir1.set_Birim("ADET");
                satir1.set_Satir_Id(Guid.NewGuid().ToString());
                satir1.set_Satir_Sirano(1);


                if (fatura.Fatura_Kaydet())
                {
                    MessageBox.Show("Fatura kaydı tamamlanmıştır !...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Fatura kaydı oluşturulamamıştır !..." +
                        Environment.NewLine +
                        fatura.get_HataMesaji(), "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                string eklemesaj = string.Empty;
                if (fatura != null)
                    eklemesaj = fatura.get_HataMesaji();

                MessageBox.Show(ex.Message + Environment.NewLine + eklemesaj);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ZirveConnect = new Zirve503Islemler();
            ZirveConnect.set_SqlAdi(tbSqlAdi.Text);  //"V3772G\\SQL2008"
            ZirveConnect.set_SqlKullaniciAdi(tbKullaniciAdi.Text); //"zirvenet"
            ZirveConnect.set_SqlSifre(tbSifre.Text); //"zrvsql"
            ZirveConnect.set_FirmaKlavuzAdi(tbKlavuzAdi.Text); //"ZRVLIB_TEST"
            ZirveConnect.set_CalismaYili(tbYil.Text); //"2015"
            ZirveConnect.set_MusteriNo(tbMusteriNo.Text);
            ZirveConnect.SqleBaglan();

            if (!string.IsNullOrEmpty(tbStokKodu1.Text))
            {
                MessageBox.Show(tbStokKodu1.Text+" bakiyesi : "+ ZirveConnect.StokBakiyesi_Ver(tbStokKodu1.Text).ToString());
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            ZirveConnect = new Zirve503Islemler();
            ZirveConnect.set_SqlAdi(tbSqlAdi.Text);  //"V3772G\\SQL2008"
            ZirveConnect.set_SqlKullaniciAdi(tbKullaniciAdi.Text); //"zirvenet"
            ZirveConnect.set_SqlSifre(tbSifre.Text); //"zrvsql"
            ZirveConnect.set_FirmaKlavuzAdi(tbKlavuzAdi.Text); //"ZRVLIB_TEST"
            ZirveConnect.set_CalismaYili(tbYil.Text); //"2015"
            ZirveConnect.set_MusteriNo(tbMusteriNo.Text);
            ZirveConnect.SqleBaglan();

            if (!string.IsNullOrEmpty(tbStokKodu2.Text))
            {
                MessageBox.Show(tbStokKodu2.Text + " bakiyesi : " + ZirveConnect.StokBakiyesi_Ver(tbStokKodu2.Text).ToString());
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            //
            ZirveConnect = new Zirve503Islemler();
            ZirveConnect.set_SqlAdi(tbSqlAdi.Text);  //"V3772G\\SQL2008"
            ZirveConnect.set_SqlKullaniciAdi(tbKullaniciAdi.Text); //"zirvenet"
            ZirveConnect.set_SqlSifre(tbSifre.Text); //"zrvsql"
            ZirveConnect.set_FirmaKlavuzAdi(tbKlavuzAdi.Text); //"ZRVLIB_TEST"
            ZirveConnect.set_CalismaYili(tbYil.Text); //"2015"
            ZirveConnect.set_MusteriNo(tbMusteriNo.Text);
            ZirveConnect.SqleBaglan();

            CbkIslem cbkIslem = new CbkIslem(ZirveConnect);

            List<CariKart> CariListeler = new List<CariKart>();

            CariKart CKart = new CariKart();
            CKart.set_KartKodu("Cr 001");
            CKart.set_KartAdi(" Unvan 1");
            CKart.set_cariKartturu(CariKartTuru.Alici);
            CKart.set_VergiNo("12345678901");

            CariListeler.Add(CKart);

            CariKart CKart2 = new CariKart();
            CKart2.set_KartKodu("Cr 002");
            CKart2.set_KartAdi(" Unvan 2");
            CKart2.set_cariKartturu(CariKartTuru.Alici);
            CKart2.set_VergiNo("1234500901");

            CariListeler.Add(CKart2);

            cbkIslem.set_EklenecekCariKartlar(CariListeler);


            if (cbkIslem.CariKartlariKaydet())
            {
                MessageBox.Show("Kart kaydı tamamlanmıştır !...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Kart kaydı oluşturulamamıştır !..." +
                    Environment.NewLine + cbkIslem.get_HataMesaji(), "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button12_Click(object sender, EventArgs e)
        {
            ZirveConnect = new Zirve503Islemler();
            ZirveConnect.set_SqlAdi(tbSqlAdi.Text);  //"V3772G\\SQL2008"
            ZirveConnect.set_SqlKullaniciAdi(tbKullaniciAdi.Text); //"zirvenet"
            ZirveConnect.set_SqlSifre(tbSifre.Text); //"zrvsql"
            ZirveConnect.set_FirmaKlavuzAdi(tbKlavuzAdi.Text); //"ZRVLIB_TEST"
            ZirveConnect.set_CalismaYili(tbYil.Text); //"2015"
            ZirveConnect.set_MusteriNo(tbMusteriNo.Text);
            ZirveConnect.SqleBaglan();

            CbkIslem cbkIslem = new CbkIslem(ZirveConnect);

            List<Stok> StokListeler = new List<Stok>();

            Stok CKart = new Stok(ZirveConnect);
            CKart.set_KartKodu("Stm 001");
            CKart.set_KartAdi("Kart 1");
            CKart.set_Birim("Adet");
            
            StokListeler.Add(CKart);

            Stok CKart2 = new Stok(ZirveConnect);
            CKart2.set_KartKodu("Stm 002");
            CKart2.set_KartAdi("Kart 2");
            CKart2.set_Birim("KG");

            StokListeler.Add(CKart);

            cbkIslem.set_EklenecekStokKartlar(StokListeler);


            if (cbkIslem.StokKartiKaydet())
            {
                MessageBox.Show("Kart kaydı tamamlanmıştır !...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Kart kaydı oluşturulamamıştır !..." +
                    Environment.NewLine + cbkIslem.get_HataMesaji(), "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            //

            Irsaliye irsaliye = null;

            try
            {
                ZirveConnect = new Zirve503Islemler();
                ZirveConnect.set_SqlAdi(tbSqlAdi.Text);  //"V3772G\\SQL2008"
                ZirveConnect.set_SqlKullaniciAdi(tbKullaniciAdi.Text); //"zirvenet"
                ZirveConnect.set_SqlSifre(tbSifre.Text); //"zrvsql"
                ZirveConnect.set_FirmaKlavuzAdi(tbKlavuzAdi.Text); //"ZRVLIB_TEST"
                ZirveConnect.set_CalismaYili(tbYil.Text); //"2015"
                ZirveConnect.set_MusteriNo(tbMusteriNo.Text);
                ZirveConnect.SqleBaglan();

                irsaliye = new Irsaliye(ZirveConnect);

                irsaliye.set_Turu(IrsaliyeTuru.SatisIrsaliyesi);
                irsaliye.set_EvrakNo(tbEvrakNo.Text);
                irsaliye.set_Tarih(dateTimePicker1.Value);
                irsaliye.set_Saat(dateTimePicker1.Value);
                irsaliye.set_DovizKodu("TL");

                irsaliye.set_GenelToplam(100f);
                irsaliye.set_ToplamKDV(18f);

                CariKart cariKart = irsaliye.get_CariKart();
                if (!string.IsNullOrEmpty(tbCariKodu.Text))
                {
                    cariKart.set_KartKodu(tbCariKodu.Text);
                    if (string.IsNullOrEmpty(cariKart.get_KartAdi()))
                        cariKart.set_KartAdi(tbCariAdi.Text);
                }

                irsaliye.set_Aciklama(tbAciklama.Text);

                irsaliye.set_Ozelkod1(tbOzelKod1.Text);
                irsaliye.set_Ozelkod2(tbOzelKod2.Text);

                irsaliye.set_SubeNo(1);
                irsaliye.set_DepoKodu("MERKEZ");

                ObservableCollection<IrsaliyeSatir> IrsaliyeStoklar = new ObservableCollection<IrsaliyeSatir>();
                IrsaliyeStoklar.Add(new IrsaliyeSatir(ZirveConnect));

                IrsaliyeSatir satir1 = IrsaliyeStoklar[0];
                satir1.set_KartKodu(tbStokKodu1.Text);
                if (string.IsNullOrEmpty(satir1.get_KartAdi()))
                    satir1.set_KartAdi("VODAFONE 875 SMART");
                satir1.set_Miktar(10f);
                satir1.set_DovizKodu("TL");
                satir1.set_BirimFiyatTL(14f);
                satir1.set_KDVOrani(18);
                satir1.set_IndirimOrani(0f);
                if (string.IsNullOrEmpty(satir1.get_Birim()))
                {
                    satir1.set_Birim("ADET");
                }

                satir1.set_Satir_Id(Guid.NewGuid().ToString());
                satir1.set_Satir_Sirano(1);

                satir1.set_Ozellik_1("ZRV1");
                satir1.set_Ozellik_2("zrv2");

                satir1.set_TevkifatOrani("2/10");
                satir1.set_TEVKTL(9f);

                satir1.set_Ozellik_16(16f);
                satir1.set_Ozellik_17(17f);
                satir1.set_Ozellik_18(18f);
                satir1.set_Ozellik_19(19f);
                satir1.set_Ozellik_20(20f);

                satir1.set_Ozellik_26(DateTime.Now);
                satir1.set_Ozellik_27(DateTime.Now);
                satir1.set_Ozellik_28(DateTime.Now);
                satir1.set_Ozellik_29(DateTime.Now);
                satir1.set_Ozellik_30(DateTime.Now);


                IrsaliyeStoklar.Add(new IrsaliyeSatir(ZirveConnect));

                IrsaliyeSatir satir2 = IrsaliyeStoklar[1];
                satir2.set_KartKodu(tbStokKodu2.Text);
                //tbStokKodu2.Text;
                if (string.IsNullOrEmpty(satir2.get_KartAdi()))
                    satir2.set_KartAdi("ARAÇ GİDERLERİ");
                satir2.set_Miktar(3f);
                satir2.set_DovizKodu("TL");
                satir2.set_BirimFiyatTL(8f);
                satir2.set_KDVOrani(18);
                satir2.set_IndirimOrani(10f);
                if (string.IsNullOrEmpty(satir1.get_Birim()))
                {
                    satir2.set_Birim("ADET");
                }

                
                satir2.set_Satir_Id(Guid.NewGuid().ToString());
                satir2.set_Satir_Sirano(2);

                irsaliye.set_Stoklar(IrsaliyeStoklar);

                if (irsaliye.Irsaliye_Kaydet())
                {
                    MessageBox.Show("Irsaliye kaydı tamamlanmıştır !...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {

                    MessageBox.Show("Irsaliye kaydı oluşturulamamıştır !..." +
                        Environment.NewLine +
                        irsaliye.get_HataMesaji() +
                    Environment.NewLine +
                    "", //"Kayıt sonucu bilgi : " + irsaliye.KayitSonucu.HataMesaji, 
                    "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                string eklemesaj = string.Empty;
                if (irsaliye != null)
                    eklemesaj = irsaliye.get_HataMesaji();

                MessageBox.Show(ex.Message + Environment.NewLine + eklemesaj);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            ZirveConnect = new Zirve503Islemler();
            ZirveConnect.set_SqlAdi("192.168.1.238\\ZIRVE");  //"V3772G\\SQL2008"
            ZirveConnect.set_SqlKullaniciAdi("ld"); //"zirvenet"
            ZirveConnect.set_SqlSifre("ld"); //"zrvsql"
            ZirveConnect.set_FirmaKlavuzAdi("TEST_FİRMASI"); //"ZRVLIB_TEST"
            ZirveConnect.set_CalismaYili("2022"); //"2015"
            ZirveConnect.set_MusteriNo("363348");
            ZirveConnect.SqleBaglan();

            CbkIslem cbkIslem = new CbkIslem(ZirveConnect);

            List<CariKart> CariListeler = new List<CariKart>();

            CariKart CKart = new CariKart();
            CKart.set_KartKodu("Cr 001");
            CKart.set_KartAdi(" Unvan 1");
            CKart.set_cariKartturu(CariKartTuru.Alici);
            CKart.set_VergiNo("12345678901");

            CariListeler.Add(CKart);

            CariKart CKart2 = new CariKart();
            CKart2.set_KartKodu("Cr 002");
            CKart2.set_KartAdi(" Unvan 2");
            CKart2.set_cariKartturu(CariKartTuru.Alici);
            CKart2.set_VergiNo("1234500901");

            CariListeler.Add(CKart2);

            cbkIslem.set_EklenecekCariKartlar(CariListeler);


            if (cbkIslem.CariKartlariKaydet())
            {
                MessageBox.Show("Kart kaydı tamamlanmıştır !...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Kart kaydı oluşturulamamıştır !..." +
                    Environment.NewLine + cbkIslem.get_HataMesaji(), "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Fatura fatura = null;

            try
            {
                ZirveConnect = new Zirve503Islemler();
                ZirveConnect.set_SqlAdi("192.168.1.238\\ZIRVE");  //"V3772G\\SQL2008"
                ZirveConnect.set_SqlKullaniciAdi("ld"); //"zirvenet"
                ZirveConnect.set_SqlSifre("ld"); //"zrvsql"
                ZirveConnect.set_FirmaKlavuzAdi("TEST_FİRMASI"); //"ZRVLIB_TEST"
                ZirveConnect.set_CalismaYili("2022"); //"2015"
                ZirveConnect.set_MusteriNo("363348");
                ZirveConnect.SqleBaglan();

                fatura = new Fatura(ZirveConnect);

                fatura.set_Turu(FaturaTuru.SatisFaturasi);
                fatura.set_EvrakNo(tbEvrakNo.Text);
                fatura.set_Tarih(dateTimePicker1.Value);
                fatura.set_Saat(dateTimePicker1.Value);
                fatura.set_DovizKodu("TL");

                if (!string.IsNullOrEmpty(tbTutarTL.Text))
                {
                    fatura.set_GenelToplam(float.Parse(tbTutarTL.Text));
                }
                else
                    fatura.set_GenelToplam(100f);

                if (!string.IsNullOrEmpty(tbKDV.Text))
                {
                    fatura.set_ToplamKDV(float.Parse(tbKDV.Text));
                }
                else
                    fatura.set_ToplamKDV(18f);

                CariKart cariKart = fatura.get_CariKart();
                
                cariKart.set_KartKodu("Cr 001");
                

                KasaKart kasaKart = fatura.get_KasaKart();
                BankaKart bankaKart = fatura.get_BankaKart();
                if (!string.IsNullOrEmpty(tbKasaKodu.Text))
                {
                    kasaKart.set_KartKodu(tbKasaKodu.Text);
                    if (string.IsNullOrEmpty(kasaKart.get_KartAdi()))
                        kasaKart.set_KartAdi(tbKasaAdi.Text);
                }
                else
                    if (!string.IsNullOrEmpty(tbBankaKodu.Text))
                {
                    bankaKart.set_KartKodu(tbBankaKodu.Text);
                    if (string.IsNullOrEmpty(bankaKart.get_KartAdi()))
                        bankaKart.set_KartAdi(tbBankaAdi.Text);
                }

                fatura.set_Aciklama(tbAciklama.Text);

                fatura.set_Ozelkod1(tbOzelKod1.Text);
                fatura.set_Ozelkod2(tbOzelKod2.Text);

                fatura.set_SubeNo(1);
                fatura.set_DepoKodu("MERKEZ");

                ObservableCollection<FaturaSatir> FaturaSatirlari = fatura.get_Stoklar(); // new ObservableCollection<FaturaSatir>();

                FaturaSatirlari.Add(new FaturaSatir(ZirveConnect));

                FaturaSatir satir1 = FaturaSatirlari[0]; // fatura.Stoklar[0];

                satir1.set_SatirTuru(SatirTuru.Stok);
                satir1.set_KartKodu(tbStokKodu1.Text);
                if (string.IsNullOrEmpty(satir1.get_KartAdi()))
                {
                    satir1.set_KartAdi("VODAFONE 875 SMART");
                }

                if (!string.IsNullOrEmpty(tbMiktar1.Text))
                {
                    satir1.set_Miktar(float.Parse(tbMiktar1.Text));
                }
                else
                    satir1.set_Miktar(10f);

                satir1.set_DovizKodu("TL");
                if (!string.IsNullOrEmpty(tbBrfTL1.Text))
                {
                    satir1.set_BirimFiyatTL(float.Parse(tbBrfTL1.Text));
                }
                else
                    satir1.set_BirimFiyatTL(14f);

                satir1.set_KDVOrani(18);
                satir1.set_IndirimOrani(0f);
                if (string.IsNullOrEmpty(satir1.get_Birim()))
                {
                    satir1.set_Birim("ADET");
                }
                satir1.set_Satir_Id(Guid.NewGuid().ToString());
                satir1.set_Satir_Sirano(1);

                satir1.set_TevkifatOrani("2/10");
                satir1.set_TEVKTL(9f);

                if (!string.IsNullOrEmpty(tbStokKodu2.Text))
                {
                    FaturaSatirlari.Add(new FaturaSatir(ZirveConnect));
                    FaturaSatir satir2 = FaturaSatirlari[1];
                    satir2.set_SatirTuru(SatirTuru.Stok);

                    satir2.set_KartKodu(tbStokKodu2.Text);
                    if (string.IsNullOrEmpty(satir2.get_KartAdi()))
                    {
                        satir2.set_KartAdi("ARAÇ GİDERLERİ");
                    }

                    if (!string.IsNullOrEmpty(tbMiktar2.Text))
                    {
                        satir2.set_Miktar(float.Parse(tbMiktar2.Text));
                    }
                    else
                        satir2.set_Miktar(3f);
                    satir2.set_DovizKodu("TL");

                    if (!string.IsNullOrEmpty(tbBrftl2.Text))
                    {
                        satir2.set_BirimFiyatTL(float.Parse(tbBrftl2.Text));
                    }
                    else
                        satir2.set_BirimFiyatTL(8f);
                    satir2.set_KDVOrani(18);
                    satir2.set_IndirimOrani(10f);
                    if (string.IsNullOrEmpty(satir2.get_Birim()))
                    {
                        satir2.set_Birim("ADET");
                    }
                    satir2.set_Satir_Id(Guid.NewGuid().ToString());
                    satir2.set_Satir_Sirano(2);

                }


                fatura.set_EFatura(true);
                // Not sadece Satış ve Satış iade faturalarında geçerlidir.
                fatura.set_EFaturaEvraknoVerilsin(true);


                //fatura.set_IrsaliyeTarihi(new DateTime(2016, 3, 28));
                //fatura.set_IrsaliyeNo("Irs001");


                if (fatura.Fatura_Kaydet())
                {
                    MessageBox.Show("Fatura kaydı tamamlanmıştır !..." + Environment.NewLine +
                     "Kaydedilen evraknno :" + fatura.get_KayitSonucu().get_Evrakno(), "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Fatura kaydı oluşturulamamıştır !..." +
                        Environment.NewLine +
                        fatura.get_HataMesaji() +
                      Environment.NewLine +
                      "Kayıt sonucu : " + fatura.get_KayitSonucu().get_HataMesaji(), "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                string eklemesaj = string.Empty;
                if (fatura != null)
                    eklemesaj = fatura.get_HataMesaji();

                MessageBox.Show(ex.Message + Environment.NewLine + eklemesaj);
            }
        }
    }
}
