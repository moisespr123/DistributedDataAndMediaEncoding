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
<p></p>
<?php
page_tail();
?>
