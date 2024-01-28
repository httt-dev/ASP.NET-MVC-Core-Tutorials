### install client lib use npm (nodejs)

	npm i bootstrap 
	npm i jquery
	npm i popper.js

### Copy cac thu vien client vao folder wwwroot 
	Them Thu vien BuildBundlerMinifier bang lenh sau (cmd):
		dotnet add package BuildBundlerMinifier

	Sau khi them thanh cong thi se co thong tin ve thu vien  trong file .scproj

	<PackageReference Include="BuildBundlerMinifier" Version="3.2.449" />


	De su dung thi can tao file `bundleconfig.json` trong thu muc root cua du an

    inputFiles : file nguon se duoc copy
    outputFileName : path dich se duoc copy toi 
    ```json
	[

  {
    "outputFileName": "wwwroot/css/bootstrap.min.css",
    "inputFiles": [
      "node_modules/bootstrap/dist/css/bootstrap.min.css"
    ],
    "minify": {
      "enabled": false,
      "renameLocals": true
    }
  },
  {
    "outputFileName": "wwwroot/js/bootstrap.min.js",
    "inputFiles": [
      "node_modules/bootstrap/dist/js/bootstrap.min.js"
    ],
    "minify": {
      "enabled": false,
      "renameLocals": true
    }
  },
  {
    "outputFileName": "wwwroot/js/jquery.min.js",
    "inputFiles": [
      "node_modules/jquery/dist/jquery.min.js"
    ]
  },
  {
    "outputFileName": "wwwroot/js/popper.min.js",
    "inputFiles": [
      "node_modules/popper.js/dist/popper.min.js"
    ],
    "minify": {
      "enabled": false,
      "renameLocals": true
    }

  }
]
	
```