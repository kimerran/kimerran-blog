=begin
  * Name: parse.rb
  * Description: utility script for kimeblog to generate all.json
  * Author: mh.neri@gmail.com
  * Date: 8-June-2014
  * License: Unlicensed
=end
out_f = File.new("all.json", "w")
out_f.puts("[")

Dir.foreach("posts") { |x|
	if /^[0-9]*.md$/.match(x)
		contents = File.read("#{Dir.pwd}/posts/#{x}")	
		meta =  /%(\{[^\}]+\})%[^*]+/.match(contents).captures		
		out_f.puts(meta)
		out_f.print(",")
	end
}
out_f.puts("]")
out_f.close
