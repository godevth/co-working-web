using CoreCMS.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCMS.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        #region [Tables]
        public DbSet<SystemVariableCategory> SystemVariableCategories { get; set; }
        public DbSet<SystemVariable> SystemVariables { get; set; }

        public DbSet<Picture> Picture { get; set; }
        public DbSet<PlaceType> PlaceType { get; set; }
        public DbSet<RoomType> RoomType { get; set; }
        public DbSet<Place> Place { get; set; }
        public DbSet<ImplementationDate> ImplementationDate { get; set; }
        public DbSet<FacilityType> FacilityType { get; set; }
        public DbSet<Facility> Facility { get; set; }
        public DbSet<Room> Room { get; set; }
        public DbSet<FacilityServices> FacilityServices { get; set; }
        public DbSet<UserType> UserType { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<CompanyProfiles> CompanyProfiles { get; set; }
        public DbSet<Booking> Booking { get; set; }
        public DbSet<BookingDate> BookingDate { get; set; }
        public DbSet<BookingFacility> BookingFacility { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Badge> Badges { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<LogNotification> LogNotification { get; set; }
        public DbSet<RoomPrice> RoomPrice { get; set; }
        public DbSet<MessageNotification> MessageNotification { get; set; }
        public DbSet<CheckIn> CheckIn { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<IoTDeviceGroup> IoTDeviceGroup { get; set; }
        public DbSet<IoTDevice> IoTDevice { get; set; }
        public DbSet<IoTTransaction> IoTTransaction { get; set; }
        public DbSet<WishlistUserMapping> WishlistUserMapping { get; set; }
        public DbSet<Privilege> Privilege { get; set; }
        public DbSet<PlacePaymentMethod> PlacePaymentMethod { get; set; }
        public DbSet<TermAndCondition> TermAndCondition { get; set; }
        public DbSet<UserConsent> UserConsent { get; set; }
        public DbSet<PlaceTheme> PlaceTheme { get; set; }
        public DbSet<UserTheme> UserTheme { get; set; }
        public DbSet<PaymentResponse> PaymentResponse { get; set; }

        #endregion

        #region View
        public DbQuery<UserSearchView> UserSearchView { get; set; }
        public DbQuery<PlaceRoomView> PlaceRoomView { get; set; }
        public DbQuery<BookingView> BookingView { get; set; }
        public DbQuery<HistoryView> HistoryView { get; set; }
        public DbQuery<UserConsentPersistedGrantsView> UserConsentPersistedGrantsView { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region [Init Users & Roles]
            // any guid
            string ADMIN_ID = "1312f09c-4010-429d-89cf-c6742832eec9";
            // any guid, but nothing is against to use the same one
            string ROLE_ID = "7fd2efe6-e036-4004-9a9b-0fdd9a089d03";
            //Guid.NewGuid().ToString()
            builder.Entity<ApplicationRole>().HasData(new ApplicationRole
            {
                Id = ROLE_ID,
                Name = "super_admin",
                NormalizedName = "SUPER_ADMIN",
                Description = "ผู้ดูแลระบบสูงสุด",
                UserTypeId = 1
            },
            new ApplicationRole
            {
                Id = "45542588-db7b-4efc-97a7-91f11989d26f",
                Name = "admin",
                NormalizedName = "ADMIN",
                Description = "ผู้ดูแลระบบ",
                UserTypeId = 1
            },
            new ApplicationRole
            {
                Id = "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                Name = "business_owner",
                NormalizedName = "BUSINESS_OWNER",
                Description = "เจ้าของกิจการ",
                UserTypeId = 2
            },
            new ApplicationRole
            {
                Id = "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                Name = "system",
                NormalizedName = "SYSTEM",
                Description = "พนักงานดูแลระบบ",
                UserTypeId = 2
            },
            new ApplicationRole
            {
                Id = "afabc5ea-07e3-4021-8026-258c371558ed",
                Name = "bronze",
                NormalizedName = "BRONZE",
                Description = "สมาชิกระดับ Bronze",
                UserTypeId = 3
            },
            new ApplicationRole
            {
                Id = "5744dd30-16b8-40ac-9ace-00aca29976ce",
                Name = "silver",
                NormalizedName = "SILVER",
                Description = "สมาชิกระดับ Silver",
                UserTypeId = 3
            },
            new ApplicationRole
            {
                Id = "2419e2fd-c635-412d-b689-338ea3f13b32",
                Name = "gold",
                NormalizedName = "GOLD",
                Description = "สมาชิกระดับ Gold",
                UserTypeId = 3
            },
            new ApplicationRole
            {
                Id = "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                Name = "platinum",
                NormalizedName = "PLATINUM",
                Description = "สมาชิกระดับ Platinum",
                UserTypeId = 3
            });

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ROLE_ID,
                UserId = ADMIN_ID
            });
            #endregion

            #region UserType
            builder.Entity<UserType>().HasData(new UserType
            {
                UserTypeId = 1,
                Name = "admin",
                NormalizedName = "ADMIN",
                Description = "ผู้ดูแลระบบ"
            },
           new UserType
           {
               UserTypeId = 2,
               Name = "shop",
               NormalizedName = "SHOP",
               Description = "ร้านค้า"
           },
           new UserType
           {
               UserTypeId = 3,
               Name = "general_member",
               NormalizedName = "GENERAL_MEMBER",
               Description = "สมาชิกทั่วไป"
           },
           new UserType
           {
               UserTypeId = 4,
               Name = "guest",
               NormalizedName = "GUEST",
               Description = "ผู้ใช้งานทั่วไป (Guest)"
           },
           new UserType
           {
               UserTypeId = 5,
               Name = "juristic",
               NormalizedName = "JURISTIC",
               Description = "นิติบุคคล, องค์กรเอกชน, องค์กร์ภาครัฐ"
           });
            #endregion

            #region [Init System Variables]
            builder.Entity<SystemVariableCategory>().HasData(
                new SystemVariableCategory()
                {
                    SystemVariableCategoryCode = "MEMBER_CLASS",
                    SystemVariableCategoryName = "ระดับบัตรสมาชิก"
                },
                new SystemVariableCategory()
                {
                    SystemVariableCategoryCode = "PAYMENT_METHOD",
                    SystemVariableCategoryName = "วิธีการชำระเงิน"
                },
                new SystemVariableCategory()
                {
                    SystemVariableCategoryCode = "BOOKING_STATUS",
                    SystemVariableCategoryName = "สถานะการจอง"
                },
                new SystemVariableCategory()
                {
                    SystemVariableCategoryCode = "BANK",
                    SystemVariableCategoryName = "ธนาคาร"
                },
                new SystemVariableCategory()
                {
                    SystemVariableCategoryCode = "COUNTER_PAYMENT",
                    SystemVariableCategoryName = "วิธีชำระเงินหน้าเค้าท์เตอร์"
                },
                new SystemVariableCategory()
                {
                    SystemVariableCategoryCode = "CREDIT_CARD_TYPE",
                    SystemVariableCategoryName = "ประเภทบัตรเครดิต"
                },
                new SystemVariableCategory()
                {
                    SystemVariableCategoryCode = "IOT_DEVICE_STATUS",
                    SystemVariableCategoryName = "สถานะของอุปกรณ์"
                },
                new SystemVariableCategory()
                {
                    SystemVariableCategoryCode = "IOT_DEVICE_TYPE",
                    SystemVariableCategoryName = "ประเภทของอุปกรณ์"
                },
                new SystemVariableCategory()
                {
                    SystemVariableCategoryCode = "PRIVILEGE_TYPE",
                    SystemVariableCategoryName = "ประเภทของสิทธิพิเศษ"
                },
                new SystemVariableCategory()
                {
                    SystemVariableCategoryCode = "SEEING_TYPE",
                    SystemVariableCategoryName = "ประเภทการมองเห็น"
                },
                new SystemVariableCategory()
                {
                    SystemVariableCategoryCode = "THEME_TYPE",
                    SystemVariableCategoryName = "ประเภทของธีม"
                },
                new SystemVariableCategory()
                {
                    SystemVariableCategoryCode = "VOID_TYPE",
                    SystemVariableCategoryName = "ประเภทการคืนเงิน"
                }
            );

            builder.Entity<SystemVariable>().HasData(
            #region MEMBER_CLASS
                new SystemVariable()
                {
                    SystemVariableCategoryCode = "MEMBER_CLASS",
                    SystemVariableCode = "MEMBER_CLASS_NORMAL",
                    SystemVariableName = "สมาชิกทั่วไป",
                    Ordering = 1
                },
                new SystemVariable()
                {
                    SystemVariableCategoryCode = "MEMBER_CLASS",
                    SystemVariableCode = "MEMBER_CLASS_GOLD",
                    SystemVariableName = "สมาชิกบัตรทอง",
                    Ordering = 2
                },
            #endregion
            #region PAYMENT_METHOD
                new SystemVariable()
                {
                    SystemVariableCategoryCode = "PAYMENT_METHOD",
                    SystemVariableCode = "PAYMENT_METHOD_COD",
                    SystemVariableName = "Pay at the front desk",
                    Ordering = 1
                },
                new SystemVariable()
                {
                    SystemVariableCategoryCode = "PAYMENT_METHOD",
                    SystemVariableCode = "PAYMENT_METHOD_ONLINE",
                    SystemVariableName = "Payment Online",
                    Ordering = 2
                },
            #endregion
            #region BOOKING_STATUS
                new SystemVariable()
                {
                    SystemVariableCategoryCode = "BOOKING_STATUS",
                    SystemVariableCode = "BOOKING_STATUS_RESERVE",
                    SystemVariableName = "จอง",
                    Ordering = 1
                },
                new SystemVariable()
                {
                    SystemVariableCategoryCode = "BOOKING_STATUS",
                    SystemVariableCode = "BOOKING_STATUS_WAITING_FOR_PAYMENT",
                    SystemVariableName = "รอชำระเงิน",
                    Ordering = 2
                },
                new SystemVariable()
                {
                    SystemVariableCategoryCode = "BOOKING_STATUS",
                    SystemVariableCode = "BOOKING_STATUS_PAID",
                    SystemVariableName = "ชำระเงินแล้ว",
                    Ordering = 3
                },
                new SystemVariable()
                {
                    SystemVariableCategoryCode = "BOOKING_STATUS",
                    SystemVariableCode = "BOOKING_STATUS_WAITING_FOR_CHECKIN",
                    SystemVariableName = " รอ Check In",
                    Ordering = 4
                },
                new SystemVariable()
                 {
                     SystemVariableCategoryCode = "BOOKING_STATUS",
                     SystemVariableCode = "BOOKING_STATUS_CHECKIN",
                     SystemVariableName = "เข้าใช้งาน",
                     Ordering = 5
                 },
                new SystemVariable()
                {
                    SystemVariableCategoryCode = "BOOKING_STATUS",
                    SystemVariableCode = "BOOKING_STATUS_COMPLETE",
                    SystemVariableName = "สมบูรณ์",
                    Ordering = 6
                },
                new SystemVariable()
                {
                    SystemVariableCategoryCode = "BOOKING_STATUS",
                    SystemVariableCode = "BOOKING_STATUS_CANCEL",
                    SystemVariableName = "ยกเลิกการจอง",
                    Ordering = 7
                },
                new SystemVariable()
                {
                    SystemVariableCategoryCode = "BOOKING_STATUS",
                    SystemVariableCode = "BOOKING_STATUS_PLACE_CANCEL",
                    SystemVariableName = "ถูกยกเลิกการจอง",
                    Ordering = 8
                },
            #endregion
            #region BANK
                new SystemVariable()
                {
                    SystemVariableCategoryCode = "BANK",
                    SystemVariableCode = "BANK_BBL",
                    SystemVariableName = "ธนาคารกรุงเทพ",
                    Ordering = 1
                },
                new SystemVariable()
                {
                    SystemVariableCategoryCode = "BANK",
                    SystemVariableCode = "BANK_KBANK",
                    SystemVariableName = "ธนาคารกสิกรไทย",
                    Ordering = 2
                },
                new SystemVariable()
                {
                    SystemVariableCategoryCode = "BANK",
                    SystemVariableCode = "BANK_KTB",
                    SystemVariableName = "ธนาคารกรุงไทย",
                    Ordering = 3
                },
                new SystemVariable()
                {
                    SystemVariableCategoryCode = "BANK",
                    SystemVariableCode = "BANK_TMB",
                    SystemVariableName = "ธนาคารทหารไทย",
                    Ordering = 4
                },
                new SystemVariable()
                {
                    SystemVariableCategoryCode = "BANK",
                    SystemVariableCode = "BANK_SCB",
                    SystemVariableName = "ธนาคารไทยพาณิชย์",
                    Ordering = 5
                },
                new SystemVariable()
                {
                    SystemVariableCategoryCode = "BANK",
                    SystemVariableCode = "BANK_BAY",
                    SystemVariableName = "ธนาคารกรุงศรีอยุธยา",
                    Ordering = 6
                },
                new SystemVariable()
                {
                    SystemVariableCategoryCode = "BANK",
                    SystemVariableCode = "BANK_KKP",
                    SystemVariableName = "ธนาคารเกียรตินาคินภัทร",
                    Ordering = 7
                },
                new SystemVariable()
                {
                    SystemVariableCategoryCode = "BANK",
                    SystemVariableCode = "BANK_CIMBT",
                    SystemVariableName = "ธนาคารซีไอเอ็มบีไทย",
                    Ordering = 8
                },
                new SystemVariable()
                {
                    SystemVariableCategoryCode = "BANK",
                    SystemVariableCode = "BANK_TISCO",
                    SystemVariableName = "ธนาคารทิสโก้",
                    Ordering = 9
                },
                new SystemVariable()
                {
                    SystemVariableCategoryCode = "BANK",
                    SystemVariableCode = "BANK_TBANK",
                    SystemVariableName = "ธนาคารธนชาต",
                    Ordering = 10
                },
                new SystemVariable()
                {
                    SystemVariableCategoryCode = "BANK",
                    SystemVariableCode = "BANK_UOBT",
                    SystemVariableName = "ธนาคารยูโอบี",
                    Ordering = 11
                },
                new SystemVariable()
                {
                    SystemVariableCategoryCode = "BANK",
                    SystemVariableCode = "BANK_TCD",
                    SystemVariableName = "ธนาคารไทยเครดิตเพื่อรายย่อย",
                    Ordering = 12
                },
                new SystemVariable()
                {
                    SystemVariableCategoryCode = "BANK",
                    SystemVariableCode = "BANK_LHFG",
                    SystemVariableName = "ธนาคารแลนด์แอนด์ เฮ้าส์",
                    Ordering = 13
                },
                new SystemVariable()
                {
                    SystemVariableCategoryCode = "BANK",
                    SystemVariableCode = "BANK_BAAC",
                    SystemVariableName = "ธนาคารเพื่อการเกษตรและสหกรณ์การเกษตร",
                    Ordering = 14
                },
                new SystemVariable()
                {
                    SystemVariableCategoryCode = "BANK",
                    SystemVariableCode = "BANK_GSB",
                    SystemVariableName = "ธนาคารออมสิน",
                    Ordering = 15
                },
            #endregion
            #region COUNTER_PAYMENT
                new SystemVariable()
                {
                    SystemVariableCategoryCode = "COUNTER_PAYMENT",
                    SystemVariableCode = "COUNTER_PAYMENT_CASH",
                    SystemVariableName = "เงินสด",
                    Ordering = 1
                },
                new SystemVariable()
                {
                    SystemVariableCategoryCode = "COUNTER_PAYMENT",
                    SystemVariableCode = "COUNTER_PAYMENT_TRANSFER",
                    SystemVariableName = "เงินโอน",
                    Ordering = 2
                },
                new SystemVariable()
                {
                    SystemVariableCategoryCode = "COUNTER_PAYMENT",
                    SystemVariableCode = "COUNTER_PAYMENT_CREDIT_CARD",
                    SystemVariableName = "บัตรเครดิต",
                    Ordering = 3
                },
            #endregion
            #region CREDIT_CARD_TYPE
                new SystemVariable()
                {
                    SystemVariableCategoryCode = "CREDIT_CARD_TYPE",
                    SystemVariableCode = "CREDIT_CARD_TYPE_VISA",
                    SystemVariableName = "VISA",
                    Ordering = 1
                },
                new SystemVariable()
                {
                    SystemVariableCategoryCode = "CREDIT_CARD_TYPE",
                    SystemVariableCode = "CREDIT_CARD_TYPE_MASTER_CARD",
                    SystemVariableName = "Master Card",
                    Ordering = 2
                },
                new SystemVariable()
                {
                    SystemVariableCategoryCode = "CREDIT_CARD_TYPE",
                    SystemVariableCode = "CREDIT_CARD_TYPE_JCB",
                    SystemVariableName = "JCB",
                    Ordering = 3
                },
                new SystemVariable()
                {
                    SystemVariableCategoryCode = "CREDIT_CARD_TYPE",
                    SystemVariableCode = "CREDIT_CARD_TYPE_OTHER",
                    SystemVariableName = "อื่นๆ",
                    Ordering = 4
                },
            #endregion
            #region IOT_DEVICE_STATUS
            new SystemVariable()
            {
                SystemVariableCategoryCode = "IOT_DEVICE_STATUS",
                SystemVariableCode = "IOT_DEVICE_STATUS_ON",
                SystemVariableName = "เปิด",
                Ordering = 1
            },
            new SystemVariable()
            {
                SystemVariableCategoryCode = "IOT_DEVICE_STATUS",
                SystemVariableCode = "IOT_DEVICE_STATUS_OFF",
                SystemVariableName = "ปิด",
                Ordering = 2
            },
            #endregion
            #region IOT_DEVICE_TYPE
            new SystemVariable()
            {
                SystemVariableCategoryCode = "IOT_DEVICE_TYPE",
                SystemVariableCode = "IOT_DEVICE_TYPE_LIGHT",
                SystemVariableName = "ไฟ",
                Ordering = 1
            },
            new SystemVariable()
            {
                SystemVariableCategoryCode = "IOT_DEVICE_TYPE",
                SystemVariableCode = "IOT_DEVICE_TYPE_PLUG",
                SystemVariableName = "ปลั๊ก",
                Ordering = 2
            },
            #endregion
            #region PRIVILEGE_TYPE
            new SystemVariable()
            {
                SystemVariableCategoryCode = "PRIVILEGE_TYPE",
                SystemVariableCode = "PRIVILEGE_TYPE_DOMAIN",
                SystemVariableName = "โดเมน",
                Ordering = 1
            },
            new SystemVariable()
            {
                SystemVariableCategoryCode = "PRIVILEGE_TYPE",
                SystemVariableCode = "PRIVILEGE_TYPE_PERSON",
                SystemVariableName = "ผู้ใช้งาน",
                Ordering = 2
            },
            #endregion
            #region SEEING_TYPE
            new SystemVariable()
            {
                SystemVariableCategoryCode = "SEEING_TYPE",
                SystemVariableCode = "SEEING_TYPE_PUBLIC",
                SystemVariableName = "Public",
                Ordering = 1
            },
            new SystemVariable()
            {
                SystemVariableCategoryCode = "SEEING_TYPE",
                SystemVariableCode = "SEEING_TYPE_PRIVATE",
                SystemVariableName = "Private",
                Ordering = 2
            },
            new SystemVariable()
            {
                SystemVariableCategoryCode = "SEEING_TYPE",
                SystemVariableCode = "SEEING_TYPE_PRIVATE_ONLY",
                SystemVariableName = "Private Only",
                Ordering = 3
            },
            #endregion
            #region THEME_TYPE
            new SystemVariable()
            {
                SystemVariableCategoryCode = "THEME_TYPE",
                SystemVariableCode = "THEME_TYPE_LOGO_LIGHT",
                SystemVariableName = "รูปโลโก้แบบสว่าง",
                Ordering = 1
            },
            new SystemVariable()
            {
                SystemVariableCategoryCode = "THEME_TYPE",
                SystemVariableCode = "THEME_TYPE_LOGO_DARK",
                SystemVariableName = "รูปโลโก้แบบมืด",
                Ordering = 2
            },
            new SystemVariable()
            {
                SystemVariableCategoryCode = "THEME_TYPE",
                SystemVariableCode = "THEME_TYPE_BG_LIGHT",
                SystemVariableName = "รูปพื้นหลังแบบสว่าง",
                Ordering = 3
            },
            new SystemVariable()
            {
                SystemVariableCategoryCode = "THEME_TYPE",
                SystemVariableCode = "THEME_TYPE_BG_DARK",
                SystemVariableName = "รูปพื้นหลังแบบมืด",
                Ordering = 4
            },
            #endregion
            #region VOID_TYPE
            new SystemVariable()
            {
                SystemVariableCategoryCode = "VOID_TYPE",
                SystemVariableCode = "VOID_TYPE_IN_PROGRESS",
                SystemVariableName = "อยู่ระหว่างดำเนินการ",
                Ordering = 1
            },
            new SystemVariable()
            {
                SystemVariableCategoryCode = "VOID_TYPE",
                SystemVariableCode = "VOID_TYPE_REFUNDED",
                SystemVariableName = "คืนเงิน",
                Ordering = 2
            }
            #endregion
            );
            #endregion

            #region Picture
            builder.Entity<Picture>(o =>
            {
                o.HasIndex(p => p.PictureId);
                o.HasIndex(p => p.CodeRef);
                o.HasIndex(p => p.TypeRef);
                o.HasIndex(p => p.InActiveStatus);
            });
            #endregion

            #region UserType
            builder.Entity<UserType>(o =>
            {
                o.HasIndex(p => p.UserTypeId);
                o.HasIndex(p => p.NormalizedName);
            });
            #endregion

            #region Booking
            builder.Entity<Booking>(o =>
            {
                o.HasIndex(p => p.BookingId);
                o.HasIndex(p => p.IsDeleted);
                o.HasIndex(p => p.InActiveStatus);
            });
            #endregion

            #region Booking
            builder.Entity<Booking>(o =>
            {
                o.HasIndex(p => p.BookingId);
                o.HasIndex(p => p.BookingNumber);
                o.HasIndex(p => p.IsDeleted);
                o.HasIndex(p => p.InActiveStatus);
                o.HasIndex(p => new { p.BookingNumber }).IsUnique();
            });
            #endregion

            #region BookingDate
            builder.Entity<BookingDate>(o =>
            {
                o.HasIndex(p => p.BookingDateId);
                o.HasIndex(p => p.BookingId);
            });
            #endregion

            #region BookingFacility
            builder.Entity<BookingFacility>(o =>
            {
                o.HasIndex(p => p.BookingFacilityId);
                o.HasIndex(p => p.FacilityId);
            });
            #endregion

            #region CheckIn
            builder.Entity<CheckIn>(o =>
            {
                o.HasIndex(p => p.CheckInId);
                o.HasIndex(p => p.BookingId);
                o.HasIndex(p => p.IsDeleted);
                o.HasIndex(p => p.InActiveStatus);
            });
            #endregion

            #region Payment
            builder.Entity<Payment>(o =>
            {
                o.HasIndex(p => p.PaymentId);
                o.HasIndex(p => p.BookingId);
                o.HasIndex(p => p.IsDeleted);
                o.HasIndex(p => p.InActiveStatus);
            });
            #endregion

            #region IoTDeviceGroup
            builder.Entity<IoTDeviceGroup>(o =>
            {
                o.HasIndex(p => p.IoTDeviceGroupId);
                o.HasIndex(p => p.RoomId);
                o.HasIndex(p => p.IsDeleted);
                o.HasIndex(p => p.InActiveStatus);
            });
            #endregion

            #region IoTDevice
            builder.Entity<IoTDevice>(o =>
            {
                o.HasIndex(p => p.IoTDeviceId);
                o.HasIndex(p => p.IoTDeviceGroupId);
                o.HasIndex(p => p.DeviceTypeCode);
                o.HasIndex(p => p.IsDeleted);
                o.HasIndex(p => p.InActiveStatus);
            });
            #endregion

            #region IoTTransaction
            builder.Entity<IoTTransaction>(o =>
            {
                o.HasIndex(p => p.IoTTransactionId);
                o.HasIndex(p => p.IoTDeviceGroupId);
                o.HasIndex(p => p.IoTDeviceId);
                o.HasIndex(p => p.IsDeleted);
                o.HasIndex(p => p.InActiveStatus);
            });
            #endregion

            #region Privilege
            builder.Entity<Privilege>(o =>
            {
                o.HasIndex(p => p.PrivilegeId);
                o.HasIndex(p => p.PrivilegeTypeCode);
                o.HasIndex(p => p.PlaceId);
                o.HasIndex(p => p.IsDeleted);
                o.HasIndex(p => p.InActiveStatus);
            });
            #endregion

            #region PlacePaymentMethod
            builder.Entity<PlacePaymentMethod>(o =>
            {
                o.HasIndex(p => p.PlacePaymentMethodId);
                o.HasIndex(p => p.PlaceId);
            });
            #endregion

            #region TermAndCondition
            builder.Entity<TermAndCondition>(o =>
            {
                o.HasIndex(p => p.TermId);
                o.HasIndex(p => p.PlaceId);
                o.HasIndex(p => p.IsDeleted);
                o.HasIndex(p => p.InActiveStatus);
            });
            #endregion

            #region UserConsent
            builder.Entity<UserConsent>(o =>
            {
                o.HasIndex(p => p.UserConsentId);
                o.HasIndex(p => p.PlaceId);
                o.HasIndex(p => p.PersistedGrantsKey);
                o.HasIndex(p => p.IsDeleted);
                o.HasIndex(p => p.InActiveStatus);
            });
            #endregion

            #region PlaceTheme
            builder.Entity<PlaceTheme>(o =>
            {
                o.HasIndex(p => p.ThemeId);
                o.HasIndex(p => p.PlaceId);
                o.HasIndex(p => p.IsDeleted);
                o.HasIndex(p => p.InActiveStatus);
            });
            #endregion

            #region UserTheme
            builder.Entity<UserTheme>(o =>
            {
                o.HasIndex(p => p.UserThemeId);
                o.HasIndex(p => p.UserId);
                o.HasIndex(p => p.ThemeId);
                o.HasIndex(p => p.IsDeleted);
                o.HasIndex(p => p.InActiveStatus);
            });
            #endregion

            #region PaymentResponse
            builder.Entity<PaymentResponse>(o =>
            {
                o.HasIndex(p => p.Id);
                o.HasIndex(p => p.BookingId);
                o.HasIndex(p => p.MerchantID);
                o.HasIndex(p => p.BookingNumber);
                o.HasIndex(p => p.RespCode);
                o.HasIndex(p => p.RespDesc);
                o.HasIndex(p => p.ChannelCode);
            });
            #endregion
            
        }
    }
}
