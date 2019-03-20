# -*- coding: utf-8 -*-
"""
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
| FyWorld - A top down simulation game in a fantasy medieval world.    |
|                                                                      |
|    :copyright: © 2019 Florian Gasquez.                               |
|    :license: GPLv3, see LICENSE for more details.                    |
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
"""
import numpy as np
import base64
from io import BytesIO
from PIL import Image

from .point import Point
from .color import Color


class Texture2D(object):
	px_2_unit = 1

	def __init__(self, size, color=Color.empty, pivot=Point.zero):
		self.w = size.x
		self.h = size.y
		self.rw = size.x * Texture2D.px_2_unit 
		self.rh = size.y * Texture2D.px_2_unit
		self.rsize = Point(self.rw, self.rh)
		self.size = size
		self.color = color

		self.p = pivot
		self.pixels = np.full((self.rh, self.rw, 4), self.color.data, dtype=np.uint8)

	def to_image(self):
		return Image.fromarray(np.array(self.pixels), mode='RGBA')

	def to_base64(self):
		buffer_ = BytesIO()
		image = self.to_image().save(buffer_, format="png")
		return base64.b64encode(buffer_.getvalue()).decode("utf-8") 

	def get_pixel(self, x, y):
		return self.pixels[y*Texture2D.px_2_unit, x*Texture2D.px_2_unit]

	def set_pixel(self, x, y, color):
		x1 = x*Texture2D.px_2_unit
		y1 = y*Texture2D.px_2_unit

		for xx in range(x1, x1+Texture2D.px_2_unit):
			for yy in range(y1, y1+Texture2D.px_2_unit):
				self.pixels[yy, xx] = color 

	def resize(self, size):
		self.w = size.x
		self.h = size.y
		self.rw = size.x * Texture2D.px_2_unit 
		self.rh = size.y * Texture2D.px_2_unit
		self.size = size
		new_texture = np.full((self.rh, self.rw, 4), self.color.data, dtype=np.uint8)
		np.append(new_texture, self.pixels)
		self.pixels = new_texture

	def crop_empty(self):
		minX = self.rw
		minY = self.rh
		maxX = 0
		maxY = 0
		for x in range(0,self.rw):
			for y in range(0,self.rh):
				if self.pixels[y, x][3] != 0:
					if minX > x:
						minX = x
					if minY > y:
						minY = y

					if maxX < x:
						maxX = x
					if maxY < y:
						maxY = y

		self.size = Point((maxX - minX+Texture2D.px_2_unit) /  Texture2D.px_2_unit , (maxY - minY+Texture2D.px_2_unit) /  Texture2D.px_2_unit )

		self.w = self.size.x
		self.h = self.size.y
		self.rw = self.w * Texture2D.px_2_unit 
		self.rh = self.h * Texture2D.px_2_unit
		new_texture = np.full((self.rh, self.rw, 4), self.color.data, dtype=np.uint8)

		for x in range(0,self.rw):
			for y in range(0,self.rh):
				new_texture[y][x] =  self.pixels[(minY+y), (minX+x)]

		self.pixels = new_texture
		return self