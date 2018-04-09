import de.cinderella.api.cs.CindyScript;
import de.cinderella.api.cs.CindyScriptPlugin;
import org.apache.commons.math.linear.MatrixUtils;
import org.apache.commons.math.linear.RealMatrix;

import java.awt.*;
import java.util.ArrayList;
import java.util.Arrays;
import java.io.*;
import java.util.Date;

public class KetCindyPlugin extends CindyScriptPlugin {

    @CindyScript("ketjavaversion")
    public String ketjavaversion() {
        return "Ketjava 20180405";
    }

	public String getName() {
        return "KetCindy Plugin";
    }

    public String getAuthor() {
        return "The KetCindy Project Team";
    }

    @CindyScript("systemproperty")
    public String getUserID(String s) {
        return System.getProperty(s);
    }

    @CindyScript("square")
    public double quadrieren(double x) {
        return x * x;
    }

    @CindyScript("grayvalue")
    public double getGray(Color c) {
        return (c.getBlue() + c.getRed() + c.getGreen()) / 3.;
    }

    @CindyScript("isincludefull")//180331
    public static boolean isincludefull(String str){
      char[] chars = str.toCharArray();
      for (int i = 0; i < chars.length; i++) {
        if (String.valueOf(chars[i]).getBytes().length==2) {
          return true;
        }
      }
      return false;
    }
    
    @CindyScript("getdir")
    public String getdir() {
        return System.getProperty("user.dir");
    }

    @CindyScript("gethome")
    public String gethome() {
        return System.getProperty("user.home");
    }

	@CindyScript("getname")
    // 17.10.08
    public String getname() {
        return System.getProperty("user.name");
    }
	
    @CindyScript("iswindows")
    public static boolean iswindows(){
        String os=System.getProperty("os.name").toLowerCase();
        if(os!=null && os.startsWith("windows")){
            return true;
        }
        else{
            return false;
        }
    }

    @CindyScript("ismacosx")
    public static boolean ismacosx(){
        String os=System.getProperty("os.name").toLowerCase();
        if(os!=null && os.startsWith("mac")){
            return true;
        }
        else{
            return false;
        }
    }
    @CindyScript("islinux")
    public static boolean islinux(){
        String os=System.getProperty("os.name").toLowerCase();
        if(os!=null && os.startsWith("linux")){
            return true;
        }
        else{
            return false;
        }
    }

    @CindyScript("iswin")
    public static boolean iswin() {
        String OS_NAME = System.getProperty("os.name").toLowerCase();
        return OS_NAME.startsWith("windows");
    }

    @CindyScript("kc")
    public static String kc(String args){
      return "Improper call";
    }

	@CindyScript("nkfwin") //180401
    public static int nkfwin(String dir,String fname,String ext) throws IOException {
      String src=dir+File.separator+fname+"."+ext;
      String out=dir+File.separator+fname+".out";
      ProcessBuilder pb = new ProcessBuilder();
      pb.command("cmd","/c",File.separator+"nkf32","<"+src,">"+out);
      Process process = pb.start();
      return 0;
  }

  	@CindyScript("nkfcpdel") //180401
    public static int nkfcpdel(String dir,String fname,String ext) throws IOException {
      String src=dir+File.separator+fname+"."+ext;
      String out=dir+File.separator+fname+".out";
      ProcessBuilder pb = new ProcessBuilder();
      pb.command("cmd","/c","copy","/Y",out,src,"|","del",out);
      Process process = pb.start();
      return 0;
  }
  
	@CindyScript("nkfdelout") //180401
    public static int nkfdelout(String dir,String fname) throws IOException {
      String out=dir+File.separator+fname+".out";
      ProcessBuilder pb = new ProcessBuilder();
      pb.command("cmd","/c","del",out);
      Process process = pb.start();
      return 0;
  }
  
  	@CindyScript("nkfren") //180401
    public static int nkfren(String dir,String fname,String ext) throws IOException {
      String src=fname+"."+ext;
      String out=dir+File.separator+fname+".out";
      ProcessBuilder pb = new ProcessBuilder();
      pb.command("cmd","/c","ren",out,src);
      Process process = pb.start();
//      int ret = process.waitFor();
      return 0;
  }
 
    @CindyScript("kc")
    public static String kc(String args,String args2,String args3) throws IOException {
    ProcessBuilder pb = new ProcessBuilder();
    File f = new File(args);
    int flg=0;
    int pos=0;
    String kst="";
    BufferedReader br = new BufferedReader(new FileReader(f));
    String str;
	while((str = br.readLine()) != null){
      if(str.indexOf("*")>-1|| str.indexOf("?")>-1){
        flg=1;
      }
      else{
        str=str.toUpperCase();
        str=str.replaceAll("\""," "); /* 16.06.08 */
        if(str.indexOf("RM ")>-1 || str.indexOf("DEL ")>-1){
          //16.07.21from
          if(str.indexOf(".DVI")==-1 && str.indexOf(".TXT")==-1){
            flg=2;
          }
          // 16.07.21upto
        }
        if(str.indexOf("MV ")>-1 || str.indexOf("MOVE ")>-1){
          flg=3;
        }
        if(str.indexOf("RD ")>-1 || str.indexOf("RMDIR ")>-1){
          flg=4;
        }
       // 16.04.09from
        if(str.indexOf("SHUTDOWN ")>-1 || str.indexOf("SLEEP ")>-1){
          flg=5;
        }
        if(str.indexOf("CLEAR ")>-1 || str.indexOf("CLS ")>-1){
          flg=6;
        }
        if(str.indexOf("FTP ")>-1|| str.indexOf("TELNET ")>-1){
          flg=7;
        }
        if(str.indexOf("USERMOD ")>-1 || str.indexOf("USERDEL ")>-1){
          flg=8;
        }
        if(str.indexOf("USERADD	")>-1 || str.indexOf("USERMOD ")>-1){
          flg=9;
        }
        if(str.indexOf("CHMOD ")>-1){
          if(str.indexOf("CHMOD +")==-1){
            flg=10;
          }
        }
        if(str.indexOf("CRONTAB ")>-1 || str.indexOf("KILL ")>-1){
          flg=11;
        }
        if(str.indexOf("AT ")>-1){
          pos=str.indexOf("AT ");
          if(pos==0){
            flg=12;
          }
          else{
            kst=str.substring(pos-1,pos);
            if(!kst.equals("B")){
              flg=12;
            }
          }
       // 16.04.09upto
        }
//        if(str.indexOf("PATH ")>-1 || str.indexOf("PAUSE ")>-1 || str.indexOf("ARP ")>-1){
        if(str.indexOf("PAUSE ")>-1 || str.indexOf("ARP ")>-1){
          flg=13;
        }
        if(str.indexOf("RENAME ")>-1 || str.indexOf("START ")>-1){
          flg=14;
        }
        // 16.07.21
        if(str.indexOf(".SH")>-1 || str.indexOf(".BAT")>-1){
          if(str.indexOf("MAXIMA.")==-1 && str.indexOf("KC.")==-1){ /* 2016.07.22 */
            flg=15;
          }
        }
      }
      if(flg>0){
        br.close();
        return "Improper file "+String.valueOf(flg)+" "+str;
      }
    }
    br.close();
    // 16.06.05from
    Long tm=System.currentTimeMillis()-f.lastModified();
    if(tm>10000 || tm<0){ /* 16.06.08 */
      flg=50;
      return "Time expired";
    }
    if((args.indexOf("kc.")==-1|| args2.indexOf("ketlib")==-1) || args3.indexOf(".t")==-1){
      flg=60;
      return "Improper form";
    }
    if(flg==0){
      if(iswindows()){
        if((args.indexOf(" ")==-1)&&(args.indexOf("-")==-1)
                  &&(args.indexOf("　")==-1)){ //180405
          pb.command("cmd.exe","/c","start",args);
        }else{
          pb.command("cmd.exe","/c","","\""+args+"\"");//180331to
        }
      }
      else{
        if(ismacosx()){
          if(args2.indexOf("open")>-1){ /* 2016.06.07from */
            pb.command("open",args);
          }
          else{
            pb.command("sh",args);
          } /* 2016.06.07upto */
        }
        else{
          pb.command("sh",args);
        }
      }
    }
    Process process = pb.start();
    return "Normal end";
  // 16.06.05upto
  }

    @CindyScript("ispaulvisiting")
    public static boolean ispaulvisiting() {
        return true;
    }

    @CindyScript("texv")
    public static void texv( String s,  String d, String sf, String tf) throws Exception{
        ProcessBuilder pb = new ProcessBuilder();
        String[] cmd = {s,d,sf,tf};
        pb.command(cmd);
        Process process = pb.start();
        return ;
    }

    @CindyScript("givemeamatrix2")
    public static Object givemeamatrix2() {
        try {
            return "the Matrix: " + theGiveMeAMatrix().toString();
        } catch (Throwable e) {
            e.printStackTrace();
            return "no Matrix: " + e.toString();
        }
    }

    public static Object theGiveMeAMatrix() {
        double[][] matrixData = { {1d,2d,3d}, {2d,5d,3d}};
        RealMatrix m = MatrixUtils.createRealMatrix(matrixData);
        return m;
    }
    
    @CindyScript("getdirhead")
    public static String getdirhead(){
        if(iswindows()){
            return System.getProperty("user.home")+"\\ketcindy";
        }
        else if(ismacosx()){
            return System.getProperty("user.home")+"/ketcindy";
        }
        else if(islinux()){
            return System.getProperty("user.home")+"/ketcindy";
            // "/usr/share/ketcindy";
        }
        else{
            return "unknown";
        }
    }

    @CindyScript("getdirhead")
    public static String getdirhead(String dir){
        if(iswindows()){
            return dir+"\\ketcindy";
        }
        else if(ismacosx()){
            return dir+"/ketcindy";
        }
        else if(islinux()){
            return dir+"/ketcindy";
        }
        else{
            return "unknown";
        }
    }

    @CindyScript("iskcexists")
      public static boolean iskcexists(String dir){
      File file = new File(dir+"/kc.sh");
      if(file.exists()){
        file.setExecutable(true,false);
        //16.10.19
        return true;
     }
     else{
       return false;
     }
  }

    @CindyScript("pathsep")
     // 17.09.06
      public static String pathsep(){
      return File.separator;
  }

    @CindyScript("isexists")
     // 16.10.02
      public static boolean isexists(String dir,String fname){
      File file = new File(dir+File.separator+fname);
      if(file.exists()){
        return true;
     }
     else{
       return false;
     }
  }
  
      @CindyScript("filepath")
     // 18.03.29
      public static String isexists(String fname){
      File file = new File(fname);
      String path = file.getAbsolutePath();
      return path;
  }

    @CindyScript("makedir")
     // 17.09.06
      public static String makedir(String dir,String dirname){
      File newfile = new File(dir+File.separator+dirname);
      if (newfile.mkdir()){
        return dirname+" created";
      }else{
        return dirname+" already exists";
      }
  }

    @CindyScript("fileslength")
     // 16.12.09
      public static int fileslength(String dirname){
      File dir=new File(dirname);
      File[] files=dir.listFiles();
      String str;
      String head;
      int ctr=0;
      if(dir.exists()){
        for(int i=0;i<files.length;i++){
          str=files[i].getName();
          head=str.substring(0,1);
          if(head.equals(".")){ctr=ctr+1;}
        }
        return files.length-ctr;
     }
     else{
       return -1;
     }
  }
  
      @CindyScript("fileslist")
     // 16.12.09
      public static String fileslist(String dirname){
      File dir=new File(dirname);
      File[] files=dir.listFiles();
      String str;
      String head;
      String strall="";
      if(dir.exists()){
        for(int i=0;i<files.length;i++){
          str=files[i].getName();
          head=str.substring(0,1);
          if(!head.equals(".")){strall=strall+","+str;}
        }
        return strall.substring(1);
     }
     else{
       return "";
     }
  }
  
    @CindyScript("setexec")
     // 16.10.19
      public static String setexec(String dir,String fname){
      File file = new File(dir+File.separator+fname);
      File path = new File(dir);
      if(path.exists()){
        if(file.exists()){
          file.setExecutable(true,false);
          return fname+" executable";
        }
        else{
//          file.createNewFile();
//          file.setExecutable(true,false);
          return fname+" not exists";
        }
      }
      else{
        return dir+" not exists";
      }
   }

    @CindyScript("getdatetime")
     // 16.10.21
      public static String getdatetime(){
      Date date=new Date();
      return date.toString();
  }

}
