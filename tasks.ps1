<#
.Synopsis
   �������
.DESCRIPTION
   ��ϸ����
.EXAMPLE
   ���ʹ�ô� cmdlet ��ʾ��
.EXAMPLE
   ��һ�����ʹ�ô� cmdlet ��ʾ��
#>

Param
(
    # Build
    [Parameter(Mandatory=$true, ParameterSetName="Build")]
    [switch]$Build,

    # StartSite
    [Parameter(Mandatory=$true, ParameterSetName="StartSite")]
    [switch]$StartSite
)

# properties setting
$FAKE_path = ".\packages\FAKE\tools\fake.exe"
$IISExpressPath = "C:\Program Files (x86)\IIS Express\iisexpress.exe"
$appConfigPath = ".\.vs\config\applicationhost.config"
$SiteId = 2

# functions definition

function build() {
    # ensure FAKE installed
    if (-not (Test-Path $FAKE_path)) {
	    nuget install FAKE -OutputDirectory .\packages -ExcludeVersion
    }
    Invoke-Expression ($FAKE_path + " .\build.fsx")
    Pause
}

function startSite() {
    start-process $IISExpressPath -argumentlist "/config:$appConfigPath /siteid:$SiteId" -windowstyle Hidden
}

if ($Build) {
    build
}
elseif ($StartSite) {
    startSite
}

