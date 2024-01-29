### Webpack....
	https://xuanthulab.net/asp-net-core-doc-va-ghi-request-va-response-xu-ly-truy-van-co-ban-upload-file-cookie-json.html


	Ở ví dụ trước bạn đã sử dụng BuildBundlerMinifier để gộp CSS, JS. Tuy nhiên để linh hoạt hơn từ phần này sẽ sử dụng Webpack với .NET Core


	npm init -y                                         # tạo file package.json cho dự án
	npm i -D webpack webpack-cli                        # cài đặt Webpack
	npm i node-sass postcss-loader postcss-preset-env   # cài đặt các gói để làm việc với SCSS
	npm i sass-loader css-loader cssnano                # cài đặt các gói để làm việc với SCSS, CSS
	npm i mini-css-extract-plugin cross-env file-loader # cài đặt các gói để làm việc với SCSS
	npm install copy-webpack-plugin                     # cài đặt plugin copy file cho Webpack
	npm install npm-watch                               # package giám sát file  thay đổi


	npm install bootstrap                               # cài đặt thư viện bootstrap
	npm install jquery                                  # cài đặt Jquery
	npm install popper.js                               # thư viện cần cho bootstrap

	
	Sau các lệnh này các package trên được tải về lưu tại node_modules, giờ đến bước cấu hình Webpack để khi chạy nó có được mục đích sau:

		Copy jquery.min.js từ package jquery ra thư mục wwwroot/js
		Copy popper.min.js từ package popper.js ra thư mục wwwroot/js
		Copy bootstrap.min.js từ package bootstrap ra thư mục wwwroot/js
		Biên dịch file src/scss/site.scss thành file wwww/css/site.min.css (đã gộp cả CSS của Bootstrap)

	
	//Gộp Bootstrap
	// Có thể thiết lập các biến biến như màu $warning ...
	@import '../../node_modules/bootstrap/scss/bootstrap.scss';