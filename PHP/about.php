<?php
/**
 * Created by PhpStorm.
 * User: cardo
 * Date: 10/2/2018
 * Time: 5:38 PM
 */
// This file is part of BOINC.
// http://boinc.berkeley.edu
// Copyright (C) 2008 University of California
//
// BOINC is free software; you can redistribute it and/or modify it
// under the terms of the GNU Lesser General Public License
// as published by the Free Software Foundation,
// either version 3 of the License, or (at your option) any later version.
//
// BOINC is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
// See the GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License
// along with BOINC.  If not, see <http://www.gnu.org/licenses/>.

require_once("../inc/util.inc");
require_once("../inc/user.inc");

page_head(tra("About"));
?>
<p>Distributed Data and Media Encoding is a project designed to distribute data workloads among hosts to do data compression or media encoding. For example, the project can send tasks to recompress FLAC files using a series of functions that decreases the file size, but are functions that takes minutes to process.
Also, tests of new software versions can be performed by sending tasks that processes files with new, beta or alpha experimental software.</p>

<h3>FLAC Encoder</h3>
<p>Powered by Xiph's FLAC.exe, it allows encoding flac-compatible files to FLAC. The project uses several parameters that reduces the file size of FLAC's at the expense of CPU time.</p>

<h3>Opus Encoder</h3>
<p>Opus is the future of lossy audio compression. It allows you to encode your music files with very low bitrates and still produce awesome sound quality. The Opus Encoder is constantly being upgraded to the latest versions as commits comes to Xiph's Opus repositories. They are manually compiled and applied to the project. Uses Speex resampling with the maximum quality of 10 as opposed to the default quality of 5.
<h3>CMIX Compressor</h3>
This is a very high compression program at the expense of CPU time as well as memory. It can eaily use over 32GB of RAM, and it's recommended to run this with 64GB of RAM. It takes a very, very long time for compressing and decompressing files, but provides awesome results. This is an experimental compression software that's also constantly updated.

<h3>rav1e Encoder</h3>
rav1e is an AV1 video encoder. The project runs multiple video encoding tasks to speedup video encoding by using quality settings instead of speed settings. A video is first prepared by splitting it into several parts, in which tasks are created for each of these. Then, they are sent to machines to process the encoding and finally, they are returned to the server. When all of these parts have finished encoding and are returned, the video files are then merged into the resulting file.

<p></p>
<?php
page_tail();
?>
