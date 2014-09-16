infinite-story-download
=======================

c# project to download stories from infinite-story.com into html or pdf formats

This project is intended to allow users to download stories from infinite-story.com to local versions, in either html or pdf format. Html versions download each page (and graphics) separately into a folder, while the pdf generator creates a single pdf file.

it is based on the roomId of the initial starting room, which is easy to find from the URL. For example, the URL for the starting room for the excellent story Eternal by EndMaster is http://infinite-story.com/story/room.php?id=94415. So the roomId in this example is 94415.

to generate a local PDF version, use a command line: -r -p D:\Documents\myFolder\pdfName.pdf

I made this project in a few hours while watching TV, so I'm actually surprised it even works. I'll probably update this occasionally since it's publicly available now and I'm embarrassed at the quality of the code. Feel free to use it, all standard disclaimers apply, etc.


Copyright (c) 2014, Sam Grantham
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met:
    * Redistributions of source code must retain the above copyright
      notice, this list of conditions and the following disclaimer.
    * Redistributions in binary form must reproduce the above copyright
      notice, this list of conditions and the following disclaimer in the
      documentation and/or other materials provided with the distribution.
    * Neither the name of the <organization> nor the
      names of its contributors may be used to endorse or promote products
      derived from this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL <COPYRIGHT HOLDER> BE LIABLE FOR ANY
DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
