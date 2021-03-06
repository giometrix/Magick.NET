﻿// Copyright 2013-2019 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

using System.IO;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class MagickImageCollectionTests
    {
        public class TheReadMethod
        {
            [TestClass]
            public class WithByteArray
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    ExceptionAssert.ThrowsArgumentNullException("data", () =>
                    {
                        using (IMagickImageCollection images = new MagickImageCollection())
                        {
                            images.Read((byte[])null);
                        }
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    ExceptionAssert.ThrowsArgumentException("data", () =>
                    {
                        using (IMagickImageCollection images = new MagickImageCollection())
                        {
                            images.Read(new byte[0]);
                        }
                    });
                }

                [TestMethod]
                public void ShouldResetTheFormatAfterReading()
                {
                    var readSettings = new MagickReadSettings()
                    {
                        Format = MagickFormat.Png,
                    };

                    var bytes = File.ReadAllBytes(Files.CirclePNG);

                    using (IMagickImageCollection images = new MagickImageCollection())
                    {
                        images.Read(bytes, readSettings);

                        Assert.AreEqual(MagickFormat.Unknown, images[0].Settings.Format);
                    }
                }
            }

            [TestClass]
            public class WithByteArrayAndOffset
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    ExceptionAssert.ThrowsArgumentNullException("data", () =>
                    {
                        using (IMagickImageCollection images = new MagickImageCollection())
                        {
                            images.Read((byte[])null, 0, 0);
                        }
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    ExceptionAssert.ThrowsArgumentException("data", () =>
                    {
                        using (IMagickImageCollection images = new MagickImageCollection())
                        {
                            images.Read(new byte[] { }, 0, 0);
                        }
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenOffsetIsNegative()
                {
                    ExceptionAssert.ThrowsArgumentException("offset", () =>
                    {
                        using (IMagickImageCollection images = new MagickImageCollection())
                        {
                            images.Read(new byte[] { 215 }, -1, 0);
                        }
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenCountIsZero()
                {
                    ExceptionAssert.ThrowsArgumentException("count", () =>
                    {
                        using (IMagickImageCollection images = new MagickImageCollection())
                        {
                            images.Read(new byte[] { 215 }, 0, 0);
                        }
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenCountIsNegative()
                {
                    ExceptionAssert.ThrowsArgumentException("count", () =>
                    {
                        using (IMagickImageCollection images = new MagickImageCollection())
                        {
                            images.Read(new byte[] { 215 }, 0, -1);
                        }
                    });
                }

                [TestMethod]
                public void ShouldReadImage()
                {
                    using (IMagickImageCollection images = new MagickImageCollection())
                    {
                        var fileBytes = File.ReadAllBytes(Files.SnakewarePNG);
                        var bytes = new byte[fileBytes.Length + 10];
                        fileBytes.CopyTo(bytes, 10);

                        images.Read(bytes, 10, bytes.Length - 10);
                        Assert.AreEqual(1, images.Count);
                    }
                }
            }

            [TestClass]
            public class WithByteArrayAndOffsetAndMagickReadSettings
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    ExceptionAssert.ThrowsArgumentNullException("data", () =>
                    {
                        var settings = new MagickReadSettings();

                        using (IMagickImageCollection images = new MagickImageCollection())
                        {
                            images.Read(null, 0, 0, settings);
                        }
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    ExceptionAssert.ThrowsArgumentException("data", () =>
                    {
                        var settings = new MagickReadSettings();

                        using (IMagickImageCollection images = new MagickImageCollection())
                        {
                            images.Read(new byte[] { }, 0, 0, settings);
                        }
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenOffsetIsNegative()
                {
                    ExceptionAssert.ThrowsArgumentException("offset", () =>
                    {
                        var settings = new MagickReadSettings();

                        using (IMagickImageCollection images = new MagickImageCollection())
                        {
                            images.Read(new byte[] { 215 }, -1, 0, settings);
                        }
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenCountIsZero()
                {
                    ExceptionAssert.ThrowsArgumentException("count", () =>
                    {
                        var settings = new MagickReadSettings();

                        using (IMagickImageCollection images = new MagickImageCollection())
                        {
                            images.Read(new byte[] { 215 }, 0, 0, settings);
                        }
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenCountIsNegative()
                {
                    ExceptionAssert.ThrowsArgumentException("count", () =>
                    {
                        var settings = new MagickReadSettings();

                        using (IMagickImageCollection images = new MagickImageCollection())
                        {
                            images.Read(new byte[] { 215 }, 0, -1, settings);
                        }
                    });
                }

                [TestMethod]
                public void ShouldReadImage()
                {
                    var settings = new MagickReadSettings();

                    var fileBytes = File.ReadAllBytes(Files.SnakewarePNG);
                    var bytes = new byte[fileBytes.Length + 10];
                    fileBytes.CopyTo(bytes, 10);

                    using (IMagickImageCollection images = new MagickImageCollection())
                    {
                        images.Read(bytes, 10, bytes.Length - 10, settings);
                        Assert.AreEqual(1, images.Count);
                    }
                }

                [TestMethod]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    var bytes = File.ReadAllBytes(Files.CirclePNG);

                    using (IMagickImageCollection image = new MagickImageCollection())
                    {
                        image.Read(bytes, 0, bytes.Length, null);
                    }
                }
            }

            [TestClass]
            public class WithByteArrayAndMagickReadSettings
            {
                [TestMethod]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    var bytes = File.ReadAllBytes(Files.SnakewarePNG);

                    using (IMagickImageCollection images = new MagickImageCollection())
                    {
                        images.Read(bytes, null);

                        Assert.AreEqual(1, images.Count);
                    }
                }
            }

            [TestClass]
            public class WithFileInfo
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenFileInfoIsNull()
                {
                    ExceptionAssert.ThrowsArgumentNullException("file", () =>
                    {
                        using (IMagickImageCollection images = new MagickImageCollection())
                        {
                            images.Read((FileInfo)null);
                        }
                    });
                }
            }

            [TestClass]
            public class WithFileInfoAndMagickReadSettings
            {
                [TestMethod]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    var file = new FileInfo(Files.SnakewarePNG);

                    using (IMagickImageCollection images = new MagickImageCollection())
                    {
                        images.Read(file, null);

                        Assert.AreEqual(1, images.Count);
                    }
                }
            }

            [TestClass]
            public class WithFileName
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsNull()
                {
                    ExceptionAssert.ThrowsArgumentNullException("fileName", () =>
                    {
                        using (IMagickImageCollection images = new MagickImageCollection())
                        {
                            images.Read((string)null);
                        }
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    ExceptionAssert.ThrowsArgumentException("fileName", () =>
                    {
                        using (IMagickImageCollection images = new MagickImageCollection())
                        {
                            images.Read(string.Empty);
                        }
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsInvalid()
                {
                    ExceptionAssert.Throws<MagickBlobErrorException>(() =>
                    {
                        using (IMagickImageCollection images = new MagickImageCollection())
                        {
                            images.Read(Files.Missing);
                        }
                    }, "error/blob.c/OpenBlob");
                }

                [TestMethod]
                public void ShouldResetTheFormatAfterReading()
                {
                    var readSettings = new MagickReadSettings()
                    {
                        Format = MagickFormat.Png,
                    };

                    using (IMagickImageCollection input = new MagickImageCollection())
                    {
                        input.Read(Files.CirclePNG, readSettings);

                        Assert.AreEqual(MagickFormat.Unknown, input[0].Settings.Format);
                    }
                }
            }

            [TestClass]
            public class WithFileNameAndMagickReadSettings
            {
                [TestMethod]
                public void ShouldNotThrowExceptionWhenFileNameSettingsIsNull()
                {
                    using (IMagickImageCollection images = new MagickImageCollection())
                    {
                        images.Read(Files.SnakewarePNG, null);

                        Assert.AreEqual(1, images.Count);
                    }
                }
            }

            [TestClass]
            public class WithStream
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenStreamIsNull()
                {
                    ExceptionAssert.ThrowsArgumentNullException("stream", () =>
                    {
                        using (IMagickImageCollection images = new MagickImageCollection())
                        {
                            images.Read((Stream)null);
                        }
                    });
                }

                [TestMethod]
                public void ShouldResetTheFormatAfterReading()
                {
                    var readSettings = new MagickReadSettings()
                    {
                        Format = MagickFormat.Png,
                    };

                    using (var stream = File.OpenRead(Files.CirclePNG))
                    {
                        using (IMagickImageCollection input = new MagickImageCollection())
                        {
                            input.Read(stream, readSettings);

                            Assert.AreEqual(MagickFormat.Unknown, input[0].Settings.Format);
                        }
                    }
                }
            }

            [TestClass]
            public class WithStreamAndMagickReadSettings
            {
                [TestMethod]
                public void ShouldNotThrowExceptionWhenStreamSettingsIsNull()
                {
                    using (var stream = File.OpenRead(Files.SnakewarePNG))
                    {
                        using (IMagickImageCollection images = new MagickImageCollection())
                        {
                            images.Read(stream, null);

                            Assert.AreEqual(1, images.Count);
                        }
                    }
                }
            }
        }
    }
}
