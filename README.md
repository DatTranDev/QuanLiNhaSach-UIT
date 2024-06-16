# Quản lí nhà sách
![appicon](https://raw.githubusercontent.com/DatTranDev/QuanLiNhaSach-UIT/master/QuanLiNhaSach/Resources/Icon/AppIcon.ico)

## Thành viên nhóm:
- Trần Thành Đạt: 22520237
- Trần Tiến Đạt: 22520239
- Hà Minh Đức: 22520266
- Tiền Thế Dũng: 22520291
- Nguyễn Tuấn Kiệt: 21521042
## Giảng viên hướng dẫn:
- Cô Đỗ Thị Thanh Tuyền
## Mô tả sản phẩm:
- BookStore là ứng dụng quản lý đặc biệt được thiết kế cho nhà sách. Với giao diện thân thiện và đa tính năng, ứng dụng này cung cấp giải pháp toàn diện cho cả admin và nhân viên. Admin có thể dễ dàng quản lý đơn hàng, theo dõi kho hàng, quản lý nhân viên và khách hàng, cũng như xem báo cáo doanh số bán hàng. Trong khi đó, nhân viên có thể sử dụng để nhận và thanh toán đơn hàng, theo dõi hóa đơn. Với tính năng tương thích đa nền tảng, giao diện thân thiện và bảo mật cao, Book Store không chỉ giúp tối ưu hóa quy trình làm việc mà còn tạo ra trải nghiệm người dùng mượt mà, giúp nhà sách tối ưu hóa hoạt động kinh doanh của mình.
## Đối tượng sử dụng:
- Hệ thống nhà sách:
  - Chủ nhà sách: admin app
  - Nhân viên nhà sách
## Công nghệ sử dụng:
- Design UI/UX: Figma
- WPF Application C#
- Database: Microsoft SQL Server
## Link demo:
- [Youtube]()
## Hướng dẫn cài đặt:
**Developer**
- Clone repository này về máy
  `git clone https://github.com/DatTranDev/QuanLiNhaSach-UIT.git`
- Local:
  - Mở Microsoft SQL Server
  - Chạy query "QuanLiCoffeeShop.sql"
  - Vào App.config, sửa thuộc tính connectionstring từ sau "provider connection string=\&quot;" đến "\&quot;" thành "data source={your server};initial catalog={your database};integrated security=True;MultipleActiveResultSets=True;App=EntityFramework"
  - Trong đó {your server} sẽ là tên server của bạn, {your database} sẽ là tên database của bạn
  - Bấm F5 để trải nghiệm app (Nếu bạn đang ở Debug sẽ trải nghiệm được app ngay, nếu bạn đang ở Release bạn vào thư mục bin/Release để chạy file .exe)
  - Tài khoản admin mặc định: admin admin
  - Tài khoản nhân viên mặc định: minhduc196 minhduc196
# Tổng kết:
- Sản phẩm là nỗ lực hết mình của các thành viên trong nhóm. Thông qua đó, các thành viên đã học hỏi được nhiều về việc phát triển một dự án thực tế cũng như khó khăn sau này gặp phải.
- Đây là sản phẩm đầu tay của nhóm chúng tôi với lòng nhiệt huyết, đam mê công nghệ nên mong mọi người đón nhận. Mọi sai sót trong quá trình trải nghiêm app, mong mọi người có thể feedback cho đội ngũ phát triển chúng tôi tại appservice@gmail.com. Chúng tôi vô vùng cảm ơn các bạn
